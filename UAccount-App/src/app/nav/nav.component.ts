import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private authService: AuthService,
    public router : Router,
    private toastr: ToastrService) { }

  ngOnInit() {
  }
  
  loggedIn(){
    return this.authService.loggedIn();
  }

  entrar(){
    return this.router.navigate(['/user/login']);
  }

  logout(){
    localStorage.removeItem('token');
    sessionStorage.removeItem('user_id');
    sessionStorage.removeItem('user_name');

    this.toastr.show('Você não está mais logado');
    this.router.navigate(['/user/login']);
  }

  userName() {
    return sessionStorage.getItem('user_name');
  }
}
