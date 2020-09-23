import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule} from '@angular/router';
import { FontAwesomeModule, FaIconLibrary  } from '@fortawesome/angular-fontawesome';
import { faCoffee } from '@fortawesome/free-solid-svg-icons';


//COMPONENTS
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ItemListComponent } from './item-list/item-list.component';
import { ItemDetailsComponent as ItemDetailsComponent } from './item-details/item-details.component';
import { LoginComponent } from './account/login/login.component';
import { AccountService } from './account/account.service';
import { AuthInterceptor } from './account/auth-interceptor';
import { ErrorComponent } from './utils/error/error.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { UserDetailsComponent } from './user/user-details/user-details.component';
import { UserRoleDetailsComponent } from './user/user-role/user-role.component';
import { UserCommonDetailsComponent } from './user/user-common/user-common.component';
import { BoardComponent } from './board-utils/board/board.component';
import { StateComponent } from './board-utils/state/state.component';
import { DataObjectComponent } from './board-utils/data-object/data-object.component';

//PROVIDERS
import { BoardService } from './board-utils/board/board.service';
import { DataObjectService } from './board-utils/data-object/data-object.service';
import { StateService } from './board-utils/state/state.service';

// PIPES
import { OrderBy } from './board-utils/pipes/orderby.pipe';
import { Where } from './board-utils/pipes/where.pipe';
import { YesNoModalComponent } from './shared/yes-no-modal/yes-no-modal.component';
import { ModalDirective, ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ItemListComponent,
    ItemDetailsComponent,
    LoginComponent,
    ErrorComponent,
    UserListComponent,
    UserDetailsComponent,
    UserRoleDetailsComponent,
    UserCommonDetailsComponent,
    BoardComponent,
    StateComponent,
    DataObjectComponent,
    OrderBy,
    Where,
    YesNoModalComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule,
    FontAwesomeModule,
    ModalModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'item-list', component: ItemListComponent },
      { path: 'item-details/:id', component: ItemDetailsComponent },
      { path : "login", component: LoginComponent},
      { path : "users", component: UserListComponent},
      { path : 'user-details/:id', component: UserDetailsComponent},
      { path : 'board/:id', component : BoardComponent}
    ])

  ],
  providers: [AccountService, 
  {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  },
  BoardService,
  DataObjectService,
  StateService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
