//4.cpp
#include<iostream>
using namespace std;
class node{
    public:
    int v;
    node *nxt;
};

class list{
    public:
    node *head;
    node *tail;
    list(){
        head=NULL;
        tail=NULL;
    }
    ~list(){
        node *tmp;
        while(head != NULL){
            tmp = head->nxt;
            delete head;
            head=tmp;
        }
    }
    void attach(node *pnn){
        if(tail!=NULL)
            tail->nxt=pnn;
        tail=pnn;
        if(head==NULL)
            head=pnn;
    }
    void disp(){
        node *trav = head;
        while(trav!=NULL){
            cout << trav->v << " ";
            trav=trav->nxt;
        }
        cout << endl;
    }
};
void create(list *l1,list *l2){
    int max=l1->head->v,min=l1->head->v,imax,imin,i=0;
    node *trav=l1->head,*ma,*mi,*pnn;
    while(trav != NULL){
        if(trav->v > max){
            imax=i;
            max=trav->v;
            ma=trav;
        }
        if(trav->v < min){
            imin=i;
            min=trav->v;
            mi=trav;
        }
        trav=trav->nxt;
        i++;
    }
    pnn = new node;
    pnn->v = mi->v;
    pnn->nxt=NULL;
    l2->attach(pnn);
    pnn = new node;
    pnn->v = ma->v;
    pnn->nxt=NULL;
    l2->attach(pnn);
    if(imax<imin){
        trav=ma->nxt;
        while(trav!=mi){
            pnn = new node;
            pnn->v=trav->v;
            pnn->nxt=NULL;
            l2->attach(pnn);
            trav=trav->nxt;
        }
    }else{
        trav=mi->nxt;
        while(trav!=ma){
            pnn = new node;
            pnn->v=trav->v;
            pnn->nxt=NULL;
            l2->attach(pnn);
            trav=trav->nxt;
        }
    }
}
int main(){
    list *l1=new list,*l2=new list;
    node *pnn;
    int n,index;
    cin >> n;
    for (int i=0;i<n;i++){
        pnn=new node;
        cin >> pnn->v;
        pnn->nxt=NULL;
        l1->attach(pnn);
    }
    cout << "list 1: ";
    l1->disp();
    create(l1,l2);
    cout << "list 2: ";
    l2->disp();
    return 0;
}