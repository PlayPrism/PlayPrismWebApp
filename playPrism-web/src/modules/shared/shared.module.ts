import {
  DialogModule,
  DEFAULT_DIALOG_CONFIG,
  DialogConfig,
} from '@angular/cdk/dialog';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import {
  InputComponent,
  ImagePickerComponent,
  CarouselComponent,
  HeaderComponent,
  ShellComponent,
  HeaderSearchComponent,
  SignInModalComponent,
} from './components';

@NgModule({
  declarations: [
    InputComponent,
    ImagePickerComponent,
    CarouselComponent,
    HeaderComponent,
    ShellComponent,
    HeaderSearchComponent,
    SignInModalComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    DialogModule,
  ],
  exports: [
    InputComponent,
    ImagePickerComponent,
    CommonModule,
    CarouselComponent,
    HeaderComponent,
    ShellComponent,
    HeaderSearchComponent,
    SignInModalComponent,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
  ],
  providers: [
    {
      provide: DEFAULT_DIALOG_CONFIG,
      useValue: { ...new DialogConfig(), panelClass: 'p-2' },
    },
  ],
})
export class SharedModule {}
