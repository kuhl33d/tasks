//2.cpp
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
void ReverseList(list *l){
    node *nxt,*prev=NULL,*trav=l->head;
    while(trav != NULL){
        if(trav==l->head)
            l->tail=trav;
        nxt = trav->nxt;
        trav->nxt = prev;
        prev = trav;
        trav = nxt;
    }
    l->head = prev;
}
void ReverseList(list *l,list *c){
    //error if used attach
    node *nxt,*prev=NULL,*trav=l->head,*pnn;
    while(trav != NULL){
        pnn = new node;
        pnn->v = trav->v;
        pnn->nxt = prev;
        if(prev==NULL){
            c->tail = pnn;
        }
        prev=pnn;
        trav=trav->nxt;
    }
    c->head = pnn;
}
void ReverseList_easy(list *l,list *c){
    node *trav=l->head,*pnn;
    while(trav!=NULL){
        pnn = new node;
        pnn->v = trav->v;
        pnn->nxt=NULL;
        c->attach(pnn);
        trav=trav->nxt;
    }
    ReverseList(c);
}
int main(){
    list *l=new list,*l1=new list,*l2=new list;
    node *pnn;
    int n;
    cin >> n;
    for (int i=0;i<n;i++){
        pnn=new node;
        cin >> pnn->v;
        pnn->nxt=NULL;
        l->attach(pnn);
    }
    cout << "Main List: ";
    l->disp();
    ReverseList_easy(l,l1);
    cout << "List1: ";
    l1->disp();
    ReverseList(l,l2);
    cout << "List2: ";
    l2->disp();
    ReverseList(l);
    cout << "Main List: ";
    l->disp();
    return 0;
}