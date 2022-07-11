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
void total_between(list *l,int st,int en){
    node *trav = l->head;
    int total=0,flag=0;
    while(trav->v != en && trav!=NULL){
        if(flag==1){
            total += trav->v;
        }
        if(trav->v == st)
            flag=1;
        trav = trav->nxt;
    }
    if(trav == NULL)//error
        cout << "end value not found" << endl;
    else
        cout << "total " << st << ":" << en << " = " << total << endl;
}
int main(){
    list *l1 = new list,*l2 = new list;
    node *pnn;
    int n;
    cout << "list 1 number of nodes: ";
    cin >> n;
    for(int i=0;i<n;i++){
        pnn = new node;
        cin >> pnn->v;
        pnn->nxt = NULL;
        l1->attach(pnn);
    }
    cout << "list 2 number of nodes: ";
    cin >> n;
    for(int i=0;i<n;i++){
        pnn = new node;
        cin >> pnn->v;
        pnn->nxt = NULL;
        l2->attach(pnn);
    }
    node *prev,*trav=l1->head;
    int i=0;
    while(trav!=NULL){
        if(i%2){
            total_between(l2,prev->v,trav->v);
        }
        prev = trav;
        trav = trav->nxt;
        i++;
    }
    delete l1;
    delete l2;
    return 0;
}