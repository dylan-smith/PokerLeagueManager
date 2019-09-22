import {Component, NgModule, EventEmitter, Output, NgZone, OnInit} from '@angular/core';
import {MatButtonModule, MatMenuModule} from '@angular/material';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';

@Component({
  selector: 'poker-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  private mediaMatcher: MediaQueryList = matchMedia(`(max-width: 840px)`);

  @Output() SideNavToggled = new EventEmitter<void>();

  constructor(zone: NgZone) {
    this.mediaMatcher.addListener(() => zone.run(() => this.mediaMatcher = matchMedia(`(max-width: 840px)`)));
  }

  ngOnInit(): void {
  }

  toggleSideNav() {
    this.SideNavToggled.emit();
  }

  isScreenMedium(): boolean {
    return this.mediaMatcher.matches;
  }
}
