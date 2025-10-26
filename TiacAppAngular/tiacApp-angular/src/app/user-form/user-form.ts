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

  // Store response from backend
  createdUserData: any = null;

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
          this.createdUserData = response; // store returned data
        },
        error: (err) => {
          console.error('Error saving user:', err);
          alert('Failed to save user.');
        }
      });
    }
  }

  // --- Reset for new user ---
  addNewUser() {
    this.userForm.reset();
    this.socialSkills.clear();
    this.socialMedias.clear();
    this.createdUserData = null;
  }

}
