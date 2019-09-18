import { Component, OnInit, ɵConsole } from '@angular/core';
import { User } from '../_models/User';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  user: User;
  userId: any;

  constructor(private userService: UserService) {    
    this.userId = Number(sessionStorage.getItem('user_id'));
  }

  ngOnInit() {
    this.obterUsuario();
  }

  userName() {
    return sessionStorage.getItem('user_name');
  }

  obterUsuario() {
    this.userService.obterUsuario(this.userId).subscribe((_user: User) => {
      this.user = _user;
    }, error => {
      console.log(error);
    });
  }
}
