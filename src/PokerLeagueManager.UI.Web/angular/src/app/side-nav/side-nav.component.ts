import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material';

@Component({
  selector: 'poker-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit {
  private mediaMatcher: MediaQueryList = matchMedia(`(max-width: 840px)`);

  sideNavMode() : string {
    if (this.isScreenMedium()) {
      return 'over';
    } else {
      return 'side';
    }
  }

  constructor(private _router: Router, zone: NgZone) {
    this.mediaMatcher.addListener(() => zone.run(() => this.mediaMatcher = matchMedia(`(max-width: 840px)`)));
  }

  @ViewChild(MatSidenav) sidenav: MatSidenav;

  ngOnInit() {
    this._router.events.subscribe(() => {
      if (this.isScreenMedium()) {
        this.sidenav.close();
      }
    });
  }

  isScreenMedium(): boolean {
    return this.mediaMatcher.matches;
  }
}
