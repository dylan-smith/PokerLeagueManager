// import { TestBed, async } from '@angular/core/testing';
// import { AppComponent } from './app.component';
// import { SideNavComponent } from './side-nav/side-nav.component'
// import { MatButtonModule, MatExpansionModule, MatSidenavModule, MatIconModule, MatProgressSpinnerModule, MatTableModule } from '@angular/material';
// import { NavbarComponent } from './navbar/navbar.component';
// import { RouterModule } from '@angular/router';
// import { GameListComponent } from './game-list/game-list.component';
// import { InfiniteScrollModule } from './ngx-infinite-scroll/ngx-infinite-scroll';
// import { GameComponent } from './game/game.component';
// import { MomentModule } from 'angular2-moment';

// //(<any>window).pokerConfig.appInsightsKey = 'foo';

// describe('AppComponent', () => {
//   beforeEach(async(() => {
//     TestBed.configureTestingModule({
//       declarations: [
//         AppComponent,
//         SideNavComponent,
//         NavbarComponent,
//         GameListComponent,
//         GameComponent
//       ],
//       imports: [
//         MatSidenavModule,
//         MatButtonModule,
//         MatExpansionModule,
//         MatIconModule,
//         MatProgressSpinnerModule,
//         MatTableModule,
//         RouterModule.forRoot([
//           { path: '', component: GameListComponent},
//           { path: 'Home', component: GameListComponent},
//           { path: 'Games', component: GameListComponent },
//           { path: 'Stats', component: GameListComponent },
//           { path: 'POTY', component: GameListComponent },
//           { path: 'Media', component: GameListComponent },
//           { path: 'Rules', component: GameListComponent },
//           { path: '**', component: GameListComponent }
//         ]),
//         InfiniteScrollModule,
//         MomentModule
//       ]
//     }).compileComponents();
//   }));

//   it('should create the app', async(() => {
//     const fixture = TestBed.createComponent(AppComponent);
//     const app = fixture.debugElement.componentInstance;
//     expect(app).toBeTruthy();
//   }));
//   // it(`should have as title 'poker'`, async(() => {
//   //   const fixture = TestBed.createComponent(AppComponent);
//   //   const app = fixture.debugElement.componentInstance;
//   //   expect(app.title).toEqual('poker');
//   // }));
//   // it('should render title in a h1 tag', async(() => {
//   //   const fixture = TestBed.createComponent(AppComponent);
//   //   fixture.detectChanges();
//   //   const compiled = fixture.debugElement.nativeElement;
//   //   expect(compiled.querySelector('h1').textContent).toContain('Welcome to poker!');
//   // }));
// });
