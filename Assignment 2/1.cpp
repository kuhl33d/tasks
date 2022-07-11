//1.cpp
#include<iostream>
#include"list.h";
using namespace std;

void SplitList_1(list *l,int v,list *l1,list *l2){
    if(l->head->v==v){
        l1->head=NULL;
        l1->tail=NULL;
        node *trav=l->head,*pnn;
        while(trav!=NULL){
            pnn = new node;
            pnn->v = trav->v;
            pnn->nxt=NULL;
            l2->attach(pnn);
            trav=trav->nxt;
        }
        return;
    }
    node *trav = l->head,*pnn;
    while(trav->nxt->v != v){
        pnn = new node;
        pnn->v = trav->v;
        pnn->nxt=NULL;
        l1->attach(pnn);
        trav=trav->nxt;
    }
    pnn = new node;//error
    pnn->v = trav->v;
    pnn->nxt=NULL;
    l1->attach(pnn);
    trav=trav->nxt;
    while(trav!=NULL){
        pnn = new node;
        pnn->v = trav->v;
        pnn->nxt=NULL;
        l2->attach(pnn);
        trav=trav->nxt;
    }
}
void SplitList_2(list *l,int v,list *l1,list *l2){
    if(l->head->v==v){
        l1->head=NULL;
        l1->tail=NULL;
        l2->head = l->head;
        l2->tail = l->tail;
        l->head=NULL;
        l->tail=NULL;
        return;
    }
    node *trav = l->head;
    while(trav->nxt->v != v){
        trav=trav->nxt;
    }
    l1->tail = trav;
    l1->head = l->head;
    l2->head = trav->nxt;
    l2->tail = l->tail;
    trav->nxt = NULL;
    l->head=NULL;
    l->tail=NULL;
}

int main(){
    int n,v;
    list *l = new list,*l1 = new list,*l2 = new list,*l3 = new list,*l4 = new list;
    node *pnn;
    cin >> n;
    for(int i=0;i<n;i++){
        pnn = new node;
        cin >> pnn->v;
        pnn->nxt = NULL;
        l->attach(pnn);
    }
    cout << "value: ";
    cin >> v;
    SplitList_1(l,v,l1,l2);
    cout << "Main List: ";
    l->disp();
    cout << "List1: ";
    l1->disp();
    cout << "List2: ";
    l2->disp();
    SplitList_2(l,v,l3,l4);
    cout << "Main List: ";
    l->disp();
    cout << "List1: ";
    l3->disp();
    cout << "List2: ";
    l4->disp();
    return 0;
}