import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { InputComponent } from './components/input/input.component';
import { FormsModule } from '@angular/forms';
import { ImagePickerComponent } from './components/image-picker/image-picker.component';
import { CarouselComponent } from './components/carousel/carousel.component';

@NgModule({
  declarations: [InputComponent, ImagePickerComponent, CarouselComponent],
  imports: [CommonModule, HttpClientModule, FormsModule],
  exports: [
    InputComponent,
    ImagePickerComponent,
    CommonModule,
    CarouselComponent,
  ],
})
export class SharedModule {}
