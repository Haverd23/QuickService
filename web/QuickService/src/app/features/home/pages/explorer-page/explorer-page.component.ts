import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../auth/services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ServicesApiService } from '../../../services/api/services-api.service';
import { ServiceResponse } from '../../../services/interfaces/serviceResponseInterface';



@Component({
  selector: 'app-explorer-page',
  standalone: true,
  imports: [CommonModule, FormsModule ],
  templateUrl: './explorer-page.component.html',
  styleUrl: './explorer-page.component.css'
})
export class ExplorePageComponent implements OnInit {
  isLogged = false;
  currentUserId = '';

  activeTab: 'mine' | 'others' = 'others';

  searchTerm = '';
  selectedCategory = '';
  selectedCity = '';

 allServices: ServiceResponse[] = [];
  myServices: ServiceResponse[] = [];
  otherServices: ServiceResponse[] = [];
  displayedServices: ServiceResponse[] = [];

  currentPage = 1;
  pageSize = 6;
  totalItems = 0;
  totalPages = 0;

  

  categories = [
    'Mecânica',
    'Pintura',
    'Elétrica',
    'TI',
    'Hidráulica',
    'Limpeza'
  ];

  constructor(private authService: AuthService, private servicesService: ServicesApiService) {}

  ngOnInit(): void {
     this.isLogged = this.authService.isLoggin();
    this.getServices();
  }

  getServices(): void {
    this.servicesService.getPublicService().subscribe({
  next: (services: ServiceResponse[]) => {
    this.allServices = services;
    this.loadServices();
  },
  error: (error) => {
    console.error('Erro ao buscar serviços', error);
  }
});
  }

  loadServices(): void {
    this.myServices = this.allServices.filter(
      service => service.ownerId === this.currentUserId
    );

    this.otherServices = this.allServices.filter(
      service => service.ownerId !== this.currentUserId
    );

    if (!this.isLogged) {
      this.activeTab = 'others';
    }

    this.applyFilters();
  }

  setActiveTab(tab: 'mine' | 'others'): void {
    if (!this.isLogged && tab === 'mine') return;

    this.activeTab = tab;
    this.currentPage = 1;
    this.applyFilters();
  }

  applyFilters(): void {
    const term = this.searchTerm.toLowerCase().trim();
    const cityTerm = this.selectedCity.toLowerCase().trim();

    const source =
      this.activeTab === 'mine'
        ? this.myServices
        : this.otherServices;

    const filtered = source.filter(service => {
      const matchesTerm =
        !term ||
        service.title.toLowerCase().includes(term) ||
        service.description.toLowerCase().includes(term) ||
        service.category.toLowerCase().includes(term) ||
        service.city.toLowerCase().includes(term);

      const matchesCategory =
        !this.selectedCategory || service.category === this.selectedCategory;

      const matchesCity =
        !cityTerm || service.city.toLowerCase().includes(cityTerm);

      return matchesTerm && matchesCategory && matchesCity;
    });

    this.totalItems = filtered.length;
    this.totalPages = Math.ceil(this.totalItems / this.pageSize);

    if (this.totalPages === 0) {
      this.totalPages = 1;
    }

    if (this.currentPage > this.totalPages) {
      this.currentPage = 1;
    }

    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;

    this.displayedServices = filtered.slice(start, end);
  }

  onSearch(): void {
    this.currentPage = 1;
    this.applyFilters();
  }

  clearFilters(): void {
    this.searchTerm = '';
    this.selectedCategory = '';
    this.selectedCity = '';
    this.currentPage = 1;
    this.applyFilters();
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.applyFilters();
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.applyFilters();
    }
  }

  isOwner(service: ServiceResponse): boolean {
    return this.currentUserId === service.ownerId;
  }

  editService(serviceId: string): void {
    console.log('Editar serviço', serviceId);
  }

  deleteService(serviceId: string): void {
    console.log('Excluir serviço', serviceId);
  }

  showInterest(serviceId: string): void {
    console.log('Tenho interesse no serviço', serviceId);
  }
}