import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'products',
    loadChildren: () =>
      import('../products/products.module').then((m) => m.ProductsModule),
  },
  {
    path: 'profile',
    loadChildren: () => import('../user/user.module').then((m) => m.UserModule),
  },
  {
    path: 'sign-up',
    loadChildren: () => import('../auth/auth.module').then((m) => m.AuthModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
