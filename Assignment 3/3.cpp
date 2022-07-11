//3.cpp
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
void cut(list *l,list *newL){
    node *trav,*start=NULL;
    trav = l->head;
    int ishead=0;
    if(l->head->v < 0)
        ishead=1;    
    while(trav!=NULL){
        if(ishead==1 && trav->nxt->v < 0)
            break;
        if(start==NULL && trav->nxt->v < 0)
            start=trav;
        else if(trav->v < 0 && trav != start->nxt)
            break;
        trav = trav->nxt;
    }
    if(ishead){
        if(newL->head==NULL){
            newL->head = l->head;
            newL->tail = trav;
        }
        else{
            newL->tail->nxt = l->head;
            newL->tail = trav;
        }
        if(trav == l->tail){
            l->head = NULL;
            l->tail = NULL;
        }else{
            l->head=trav->nxt;
            trav->nxt = NULL;
        }
    }else{
        if(newL->head==NULL){
            newL->head = start->nxt;
            newL->tail = trav;
        }else{
            newL->tail->nxt = start->nxt;
            newL->tail = trav;
        }
        start->nxt = trav->nxt;
        trav->nxt=NULL;

        if(trav == l->tail)
            l->tail= start;
    }
}
int main(){
    int n;
    list **l = new list*[20],*newL = new list;
    node *pnn;
    for(int i=0;i<20;i++){
        l[i] = new list;
        cout << "list " << i << " number: ";
        cin >> n;
        for(int j=0;j<n;j++){
            pnn = new node;
            cin >> pnn->v;
            pnn->nxt = NULL;
            l[i]->attach(pnn);
        }
    }
    for(int i=0,j=19;i<10;i++,j--){
        cut(l[i],newL);
        cut(l[j],newL);
    }
    cout << "newL :";
    newL->disp();
    for(int i=0;i<20;i++)
        delete l[i];
    delete []l;
    delete newL;
    return 0;
}