import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { InputComponent } from './components/input/input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ImagePickerComponent } from './components/image-picker/image-picker.component';
import { HeaderComponent } from './components/header/header.component';
import { CarouselComponent } from './components/carousel/carousel.component';
import { ShellComponent } from './components/app-shell/app-shell.component';
import { RouterModule } from '@angular/router';
import { HeaderSearchComponent } from './components/header-search/header-search.component';
import { HeaderDropdownComponent } from './components/header-dropdown/header-dropdown.component';

@NgModule({
  declarations: [
    InputComponent,
    ImagePickerComponent,
    CarouselComponent,
    HeaderComponent,
    ShellComponent,
    HeaderSearchComponent,
    HeaderDropdownComponent,
  ],
  imports: [CommonModule, FormsModule, RouterModule, ReactiveFormsModule],
  exports: [
    InputComponent,
    ImagePickerComponent,
    CommonModule,
    CarouselComponent,
    HeaderComponent,
    ShellComponent,
    HeaderSearchComponent,
    HeaderDropdownComponent,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class SharedModule {}
