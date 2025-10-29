import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-form',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './user-form.html',
  styleUrl: './user-form.css',
})
export class UserForm {
  userForm: FormGroup;
  socialNetworks = ['Facebook', 'Twitter', 'Instagram', 'LinkedIn', 'GitHub'];

  createdUserData: any = null;
  allUsers: any[] = [];

  currentView: 'form' | 'created' | 'all' = 'form'; // track UI state

  constructor(private fb: FormBuilder, private userService: UserService) {
    this.userForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      socialSkills: this.fb.array([]),
      socialMedias: this.fb.array([])
    });
  }

  // --- Skills ---
  get socialSkills() {
    return this.userForm.get('socialSkills') as FormArray;
  }

  addSkill() {
    this.socialSkills.push(this.fb.control(''));
  }

  removeSkill(index: number) {
    this.socialSkills.removeAt(index);
  }

  // --- Social Media ---
  get socialMedias() {
    return this.userForm.get('socialMedias') as FormArray;
  }

  addSocialMedia() {
    this.socialMedias.push(this.fb.group({
      platform: [''],
      username: ['']
    }));
  }

  removeSocialMedia(index: number) {
    this.socialMedias.removeAt(index);
  }

  // --- Submit ---
  onSubmit() {
    if (this.userForm.valid) {
      this.userService.createUser(this.userForm.value).subscribe({
        next: (response) => {
          console.log('User saved:', response);
          this.createdUserData = response;
          this.currentView = 'created';
        },
        error: (err) => {
          console.error('Error saving user:', err);
          alert('Failed to save user.');
        }
      });
    }
  }

  // --- Reset form ---
  addNewUser() {
    this.userForm.reset();
    this.socialSkills.clear();
    this.socialMedias.clear();
    this.createdUserData = null;
    this.currentView = 'form';
  }

  // --- Show all users ---
  showAllUsers() {
    this.userService.getAllUsers().subscribe({
      next: (users) => {
        console.log('All users:', users);
        this.allUsers = users;
        this.currentView = 'all';
      },
      error: (err) => {
        console.error('Error fetching users:', err);
        alert('Failed to load users.');
      }
    });
  }

  // --- Back to form from all users ---
  backToForm() {
    this.currentView = 'form';
  }

}
