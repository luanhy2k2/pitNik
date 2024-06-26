import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './Modules/index/index.component';
import { AccountSettingComponent } from './Modules/account-setting/account-setting.component';
import { AboutComponent } from './Modules/about/about.component';
import { FriendComponent } from './Modules/friend/friend.component';
import { LoginComponent } from './Modules/login/login.component';
import { SearchComponent } from './Modules/search/search.component';

const routes: Routes = [
  {
    path: '',
    component: IndexComponent,
    title: 'Pinik'
  },
  {
    path: 'setting',
    component: AccountSettingComponent,
  },
  {
    path: 'about',
    component: AboutComponent,
  },
  {
    path: 'friends',
    component: FriendComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'search',
    component: SearchComponent,
  },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
