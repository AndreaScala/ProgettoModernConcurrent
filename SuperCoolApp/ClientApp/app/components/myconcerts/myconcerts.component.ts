import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'myconcerts',
    templateUrl: './myconcerts.component.html'
})
export class MyConcertsComponent {
    public concerts: Concert[];
    public associations: Association[];

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
    }
    //Funzione di Visualizzazione dei Concerti Prenotati tramite GET annidate.
    async show(psw: string) {
        this.http.get(this.baseUrl + 'api/user?psw=' + psw).subscribe(result => {
            let user = new User();
            user = result.json();
            this.http.get(this.baseUrl + 'api/associationss?uid=' + user.id).subscribe(result => {
                let assocList = [];
                for (let a of result.json() as Association[]) {
                    let association = new Association();
                    association.id = a.id;
                    association.uId = a.uId;
                    association.cId = a.cId;
                    association.hasChanges = false;
                    assocList.push(association);
                }
                this.associations = assocList;
                //
                let concertList: any[] = [];
                for (let a of this.associations) {
                    this.http.get(this.baseUrl + 'api/concert?id=' + a.cId).subscribe(result => {
                        /*let c = new Concert();
                        c = result.json();
                        c.hasChanges = true;
                        c.deleted = false;*/
                        //Adattamento problema Parsing JSON
                        concertList.push(result.text());
                    }, error => console.error(error));
                }
                this.concerts = concertList;
                console.log(this.concerts);
            }, error => console.error(error));
        }, error => console.error(error));
    }
}

class Concert {
    id: number;

    private _name: string = "";
    private _where: string = "";
    private _when: string = "";
    public hasChanges: boolean;
    public deleted: boolean = false;

    get name(): string {
        return this._name;
    }
    set name(n: string) {
        this._name = n;
        this.hasChanges = true;
        console.log("set name");
    }

    get where(): string {
        return this._where;
    }
    set where(w: string) {
        this._where = w;
        this.hasChanges = true;
        console.log("set where");
    }

    get when(): string {
        return this._when;
    }
    set when(w: string) {
        this._when = w;
        this.hasChanges = true;
        console.log("set when");
    }

    public toJSON() {
        return {
            id: this.id,
            name: this._name,
            where: this._where,
            when: this._when,
        };
    };
}

class User {
    id: number;

    private _name: string = "";
    private _surname: string = "";
    private _age: number = 0;
    private _password: string = "";
    public hasChanges: boolean;
    public deleted: boolean = false;

    get name(): string {
        return this._name;
    }
    set name(n: string) {
        this._name = n;
        this.hasChanges = true;
        console.log("set name");
    }

    get surname(): string {
        return this._surname;
    }
    set surname(s: string) {
        this._surname = s;
        this.hasChanges = true;
        console.log("set surname");
    }

    get age(): number {
        return this._age;
    }
    set age(a: number) {
        this._age = a;
        this.hasChanges = true;
        console.log("set age");
    }

    get password(): string {
        return this._password;
    }
    set password(p: string) {
        this._password = p;
        this.hasChanges = true;
        console.log("set password");
    }

    public toJSON() {
        return {
            id: this.id,
            name: this._name,
            surname: this._surname,
            age: this._age,
            password: this._password,
        };
    };
}

class Association {
    id: number;

    private _uid: number = 0;
    private _cid: number = 0;
    public hasChanges: boolean;
    public deleted: boolean = false;

    get uId(): number {
        return this._uid;
    }
    set uId(u: number) {
        this._uid = u;
        this.hasChanges = true;
        console.log("set uid");
    }

    get cId(): number {
        return this._cid;
    }
    set cId(c: number) {
        this._cid = c;
        this.hasChanges = true;
        console.log("set cid");
    }

    public toJSON() {
        return {
            id: this.id,
            uid: this._uid,
            cid: this._cid,
        };
    };
}