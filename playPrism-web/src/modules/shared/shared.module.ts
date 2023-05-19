import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { InputComponent } from './components/input/input.component';
import { FormsModule } from '@angular/forms';
import { ImagePickerComponent } from './components/image-picker/image-picker.component';
import { HeaderComponent } from './components/header/header.component';
import { CarouselComponent } from './components/carousel/carousel.component';
import { ShellComponent } from './components/app-shell/app-shell.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    InputComponent,
    ImagePickerComponent,
    CarouselComponent,
    HeaderComponent,
    ShellComponent,
  ],
  imports: [CommonModule, HttpClientModule, FormsModule, RouterModule],
  exports: [
    InputComponent,
    ImagePickerComponent,
    CommonModule,
    CarouselComponent,
    HeaderComponent,
    ShellComponent,
  ],
})
export class SharedModule {}
