import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'poker-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit {

  constructor() { }

  sideNavOpened : boolean = true;

  ngOnInit() {
  }

  toggleSideNav() : void {
    this.sideNavOpened = !this.sideNavOpened;
  }
}
