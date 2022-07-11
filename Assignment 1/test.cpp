#include<iostream>
using namespace std;
class node{
    public:
    int v;
    node *nxt;
    node(){
        nxt=NULL;
    }
};
class list{
    public:
    node *head;
    node *tail;
    list(){
        head=NULL;
        tail=NULL;
    }
    void push(node *pnn){
        if(tail!=NULL){
            tail->nxt = pnn;
        }
        tail = pnn;
        if(head == NULL){
            head=pnn;
        }
    }
    int calcTot(int v){
        int i=0,tot=0,j=0;
        node *trav = this->head;
        while(trav != NULL){
            if(trav->v == v)
                break;
            i++;
            trav = trav->nxt;
        }
        trav = this->head;
        while(trav != NULL){
            if(j >= i-3 && j <= i+3 && j != i)
                tot+=trav->v;
            j++;
            trav = trav->nxt;
        }
        return tot;
    }
    ~list(){
        node *tmp;
        while(head!=NULL){
            tmp = head->nxt;
            delete head;
            head = tmp;
        }
    }
};
int function1(list *l,int v){
    int tot=0;
    node *tmp[3]={NULL,NULL,NULL},*trav=l->head;
    while(trav!=NULL){
        if(trav->v==v){
            tot += tmp[0]->v+tmp[1]->v+tmp[2]->v;
            for(int i=0;i<3 && trav!=NULL;i++){
                trav=trav->nxt;
                tot+=trav->v;
            }
            break;
        }else{
            tmp[2]=tmp[1];
            tmp[1]=tmp[0];
            tmp[0]=trav;
        }
        trav=trav->nxt;
    }
    return tot;
}
int function2(list *l,int v){
    int tot=0;
    node *tmp,*trav=l->head;
    while(trav!=NULL){
        tmp=trav->nxt;
        for(int i=0;i<3;i++)
            tmp=tmp->nxt;
        if(tmp->v==v){
            trav=trav->nxt;
            for(int i=0;i<3;i++){
                tot+=trav->v;
                trav=trav->nxt;
            }
            trav=trav->nxt;
            for(int i=0;i<3;i++){
                tot+=trav->v;
                trav=trav->nxt;
            }
            break;
        }
        trav=trav->nxt;
    }
    return tot;
}
int main(){
    list *l = new list;
    char a='y';
    node *pnn;
    while(a=='y'){
        pnn = new node;
        cin >> pnn->v;
        pnn->nxt = NULL;
        l->push(pnn);
        cout << "enter another cell ?y/n" << endl;
        cin >> a;
    }
    int v;
    cout << "enter value: ";
    cin >> v;
    cout << l->calcTot(v) << endl;
    cout << function1(l,v) << endl; 
    cout << function2(l,v) << endl; 
    return 0;
}