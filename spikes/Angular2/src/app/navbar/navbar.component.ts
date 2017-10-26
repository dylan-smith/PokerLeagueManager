import {Component, NgModule, EventEmitter, Output} from '@angular/core';
import {MatButtonModule, MatMenuModule} from '@angular/material';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';

@Component({
  selector: 'poker-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent { 
  @Output() SideNavToggled = new EventEmitter<void>();

  toggleSideNav() {
    this.SideNavToggled.emit();
  }
}