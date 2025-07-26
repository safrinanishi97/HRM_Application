import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EmployeeDTO } from '../../../models/employee-dto';
import { EmployeeService } from '../../../services/employee-service';
import { ImageService } from '../../../services/image-service';
import { SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './employee-list.html',
  styleUrl: './employee-list.css'
})
export class EmployeeList implements OnInit {
  employees: EmployeeDTO[] = [];
  idClient: number = 10001001;

  constructor(private employeeService: EmployeeService,
    private imageService: ImageService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.employeeService.getAllEmployees(this.idClient).subscribe({
      next: (data) => {
        this.employees = data;
      },
      error: (err) => {
        console.error('Error fetching employees', err);
      }
    });
  }
 getImageUrl(employee: EmployeeDTO): SafeUrl | null {
  return this.imageService.convertByteArrayToImageUrl(employee.fileBase64 || null);
}

  deleteEmployee(id: number): void {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(this.idClient, id).subscribe({
        next: () => {
          this.loadEmployees();
        },
        error: (err) => {
          console.error('Error deleting employee:', err);
        }
      });
    }
  }

}
