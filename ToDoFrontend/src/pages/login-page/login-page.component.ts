import { Component } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {AuthService} from '../../services/auth.service';
import {AppMessageStore} from '../../services/app-message.store';

@Component({
  selector: 'app-login-page',
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './login-page.component.html',
  styles: ``
})
export class LoginPageComponent {
  protected isRegistering = false;
  protected loginForm: FormGroup;

  constructor(private fb: FormBuilder, public authService: AuthService, private appMessageStore: AppMessageStore) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  public toggleRegistering() {
    this.isRegistering = !this.isRegistering;
  }

  public async onSubmit() {
    if (this.loginForm.valid) {
      if (this.isRegistering) {
        await this.register();
      } else {
        await this.logIn();
      }
    }
  }

  private async logIn() {
    const email = this.loginForm.controls['email'].value;
    const password = this.loginForm.controls['password'].value;
    const success = await this.authService.logIn(email, password);

    if (success) {
      this.appMessageStore.clearMessage();
      return;
    }

    this.appMessageStore.setMessage("Incorrect email or password", 'error');
  }

  private async register() {
    const email = this.loginForm.controls['email'].value;
    const password = this.loginForm.controls['password'].value;
    const errorMessage = await this.authService.register(email, password);

    if (errorMessage) {
      this.appMessageStore.setMessage(errorMessage.toErrorMessage(), 'error');
    }
  }
}
