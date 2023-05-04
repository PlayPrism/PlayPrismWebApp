import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: InputComponent,
      multi: true,
    },
  ],
})
export class InputComponent implements ControlValueAccessor {
  @Input() title: string = '';
  @Input() editConfirmation: boolean = false;

  @ViewChild('input') input!: ElementRef;

  public editable: boolean = this.editConfirmation ? false : true;
  public value: string = '';

  public allowEdit() {
    this.editable = !this.editable;
    setTimeout(() => {
      this.input.nativeElement.focus();
    });
  }

  writeValue(value: string): void {
    this.value = value || '';
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  onInputChange(event: any): void {
    this.onChange(event.target.value);
  }

  onChange: (value: string) => void = () => {};
  onTouched: () => void = () => {};
}
