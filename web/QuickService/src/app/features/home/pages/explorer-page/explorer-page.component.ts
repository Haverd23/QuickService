import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../auth/services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';



@Component({
  selector: 'app-explorer-page',
  standalone: true,
  imports: [CommonModule, FormsModule ],
  templateUrl: './explorer-page.component.html',
  styleUrl: './explorer-page.component.css'
})
export class ExplorePageComponent implements OnInit {
  isLogged = false;
  currentUserId = '1';

  activeTab: 'mine' | 'others' = 'others';

  searchTerm = '';
  selectedCategory = '';
  selectedCity = '';

  myServices: any[] = [];
  otherServices: any[] = [];
  displayedServices: any[] = [];

  currentPage = 1;
  pageSize = 6;
  totalItems = 0;
  totalPages = 0;

  allServices = [
    {
      id: '1',
      title: 'Manutenção automotiva',
      description: 'Revisão e diagnóstico completo.',
      category: 'Mecânica',
      city: 'Recife',
      ownerId: '1',
      ownerName: 'Gabriel',
      createdAt: 'Hoje'
    },
    {
      id: '2',
      title: 'Pintura residencial',
      description: 'Pintura interna e externa.',
      category: 'Pintura',
      city: 'Olinda',
      ownerId: '2',
      ownerName: 'Carlos',
      createdAt: 'Ontem'
    },
    {
      id: '3',
      title: 'Instalação elétrica',
      description: 'Troca de tomadas, revisão elétrica e instalação de chuveiro.',
      category: 'Elétrica',
      city: 'Recife',
      ownerId: '3',
      ownerName: 'Fernanda',
      createdAt: 'Hoje'
    },
    {
      id: '4',
      title: 'Limpeza pós-obra',
      description: 'Limpeza completa de ambientes residenciais e comerciais.',
      category: 'Limpeza',
      city: 'Jaboatão',
      ownerId: '4',
      ownerName: 'Mariana',
      createdAt: '2 dias atrás'
    },
    {
      id: '5',
      title: 'Suporte técnico',
      description: 'Formatação, manutenção e instalação de programas.',
      category: 'TI',
      city: 'Recife',
      ownerId: '1',
      ownerName: 'Gabriel',
      createdAt: 'Hoje'
    },
    {
      id: '6',
      title: 'Reparo hidráulico',
      description: 'Conserto de vazamentos, torneiras e encanamentos.',
      category: 'Hidráulica',
      city: 'Olinda',
      ownerId: '5',
      ownerName: 'Rafael',
      createdAt: 'Ontem'
    },
    {
      id: '7',
      title: 'Pintura comercial',
      description: 'Pintura de fachadas e ambientes internos.',
      category: 'Pintura',
      city: 'Recife',
      ownerId: '6',
      ownerName: 'Juliana',
      createdAt: 'Hoje'
    }
  ];

  categories = [
    'Mecânica',
    'Pintura',
    'Elétrica',
    'TI',
    'Hidráulica',
    'Limpeza'
  ];

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.isLogged = this.authService.isLoggin();
    this.loadServices();
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

    let filtered = source.filter(service => {
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

  isOwner(service: any): boolean {
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