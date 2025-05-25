import { AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { AppConstants } from '../../appConstants';
import { AuthService } from '../../services/auth.service';
import { AppMessageStore } from '../../stores/app-message.store';
import { AuthStore } from '../../stores/auth.store';

@Component({
  selector: 'app-login-page',
  imports: [
    ReactiveFormsModule,
    AsyncPipe
  ],
  templateUrl: './login-page.component.html',
  styles: ``
})
export class LoginPageComponent {
  protected isRegistering = false;
  protected loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    public authService: AuthService,
    private appMessageStore: AppMessageStore,
    public authStore: AuthStore
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });

    this.authService.checkFeatureFlag(AppConstants.FeatureFlags.Register).subscribe(value => {
      this.authStore.isRegisteringEnabled = value;
    });
  }

  public async toggleRegistering() {
    const isRegisteringEnabled = await firstValueFrom(this.authStore.isRegisteringEnabled$);
    if (!isRegisteringEnabled) {
      return;
    }

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
