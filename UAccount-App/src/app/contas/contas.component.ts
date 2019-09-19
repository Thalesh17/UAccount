import { Component, OnInit } from '@angular/core';
import { ContaService } from '../_services/conta.service';
import { Conta } from '../_models/Conta';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-contas',
  templateUrl: './contas.component.html',
  styleUrls: ['./contas.component.css']
})
export class ContasComponent implements OnInit {

  titulo = 'Contas';

  registerForm: FormGroup;
  contasFiltradas: Conta[] = [];
  modoSalvar = 'post';
  contas: Conta[] = [];
  conta: Conta;
  bodyDeletarConta = '';
  _filtroLista = '';
  _userId = Number(sessionStorage.getItem('user_id'));
  vencimento = '';
  minDate: Date;

  constructor(
    private contaService: ContaService
   ,private fb: FormBuilder
   ,private localeService: BsLocaleService
   ,private toastr: ToastrService
  ) 
  {
    this.minDate = new Date();
    this.localeService.use('pt-br');
  }
  
  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.contasFiltradas = this.filtroLista ? this.filtrarContas(this.filtroLista) : this.contas;
  }

  filtrarContas(filtrarPor: string): Conta[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.contas.filter(
      conta => conta.titulo.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  openModal(template: any){
    this.registerForm.reset();
    template.show();
  }

  ngOnInit() {
    this.validation();
    this.obterContas();
  }

  obterContas() {
    this.contaService.obterContas(this._userId).subscribe((_contas: Conta[]) => {
      this.contas = _contas;
      this.contasFiltradas = this.contas;
    }, error => {
      console.log(error);
    });
  }

  novaConta(template: any){
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  excluirConta(conta: Conta, template: any) {
    this.openModal(template);
    this.conta = conta;
    this.bodyDeletarConta = `Tem certeza que deseja excluir o Evento: ${conta.titulo}, Código: ${conta.resumo}`;
  }

  confirmeDelete(template: any) {
    this.contaService.deleteConta(this.conta.id).subscribe(
      () => {
          template.hide();
          this.obterContas();
          this.toastr.success('Deletado com Sucesso');
        }, error => {
          this.toastr.error('Erro ao tentar deletar');
        }
    );
  }

  editarConta(conta: Conta, template: any){
    this.modoSalvar = 'put';
    this.openModal(template);
    this.conta = Object.assign({}, conta);
    this.registerForm.patchValue(this.conta);
  }

  validation() {
    this.registerForm = this.fb.group({
      titulo: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      resumo: ['', Validators.required],
      valor: ['', [Validators.required, Validators.max(120000)]],
      vencimento: ['', Validators.required],
    })
  }

  salvarAlteracao(template: any) {
    if(this.registerForm.valid){
        if(this.modoSalvar === 'post'){
          this.conta = Object.assign({userId: this._userId}, this.registerForm.value);
          this.contaService.postConta(this.conta).subscribe(
            (novaConta: Conta) => {
                template.hide();
                this.obterContas();
                this.toastr.success('Inserido com Sucesso');
            }, error => {
              this.toastr.success(`Erro ao inserir: ${error} `);              
            }
          );
        } else {
          this.conta = Object.assign({id: this.conta.id}, this.registerForm.value);
          this.contaService.putConta(this.conta).subscribe(
            () => {
                template.hide();
                this.obterContas();
                this.toastr.success('Editado com Sucesso');
              }, error => {
                this.toastr.success(`Erro ao editar: ${error} `);
            }
          );
        }
    }
  }

  obterStatus(vencimento : Date){
    let dataAtual = new Date();
    dataAtual.setHours(0, 0, 0, 0);
    vencimento = new Date(vencimento);
    vencimento.setHours(0, 0, 0, 0);

    let diferencaDias = this.obterDiferencaData(vencimento, dataAtual);


    if(vencimento < dataAtual){
      return `Atrasado ${diferencaDias} dias`;
    }
    else if(diferencaDias <= 5 && diferencaDias > 0){
      return `Qtd: ${diferencaDias} dias. Proximo do Vencimento.`;
    }
    else if(vencimento > dataAtual){
      return `Qtd: ${diferencaDias} dias.`;
    }
    else if(diferencaDias == 0){
      return 'Pagamento para hoje';
    }
  }

  obterDiferencaData(vencimento : Date, dataAtual : Date){
    var oneDay = 24*60*60*1000; // hours*minutes*seconds*milliseconds    
    var diffDays = Math.round(Math.abs((dataAtual.getTime() - vencimento.getTime())/(oneDay))); 

    return diffDays;
  }
  
  toDateOnly(date: any) {
    return date;
  }
}
