import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './Modules/index/index.component';
import { AccountSettingComponent } from './Modules/account-setting/account-setting.component';
import { AboutComponent } from './Modules/about/about.component';
import { FriendComponent } from './Modules/friend/friend.component';

const routes: Routes = [
  {
    path: '',
    component: IndexComponent,
    title: 'Pinik'
  },
  {
    path: 'setting',
    component: AccountSettingComponent,
    title: 'setting'
  },
  {
    path: 'about',
    component: AboutComponent,
    title: 'about'
  },
  {
    path: 'friends',
    component: FriendComponent,
    title: 'friends'
  },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
