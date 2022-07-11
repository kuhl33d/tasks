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
int main(){
    int len=30;
    // cout << "number of lists: ";
    // cin >> len;
    list **l = new list*[len];
    node *pnn,*trav,*trav2,*no;
    int n;
    for(int i=0;i<len;i++){
        l[i] = new list;
        cout << "list " << i << " number of nodes: ";
        cin >> n;
        for(int j=0;j<n;j++){
            pnn = new node;
            cin >> pnn->v;
            pnn->nxt = NULL;
            l[i]->attach(pnn);
        }
    }
    int ct,j;
    for(int i=0;i<len;i+=2){
        ct=0,j=0;
        trav = l[i]->head;
        trav2 = l[i+1]->head;
        if(trav->v == -1){
            l[i+1]->head = trav2->nxt;
            trav2->nxt = trav->nxt;
            trav->nxt = trav2;
            continue;
        }
        while(trav!=NULL){
            if(trav->nxt->v==-1)
                break;
            ct++;
            trav=trav->nxt;
        }
        while(trav2!=NULL){
            if(j==ct)
                break;
            j++;
            trav2=trav2->nxt;
        }
        if(trav2==NULL){
            cout << "index out of range" << endl;
            continue;
        }
        no = trav2->nxt;
        trav2->nxt = trav2->nxt->nxt;
        if(no == l[i+1]->tail)
            l[i+1]->tail = trav2;
        no->nxt = trav->nxt->nxt;
        trav->nxt->nxt = no;
    }
    for(int i=0;i<len;i++){
        l[i]->disp();
    }
    for(int i=0;i<len;i++){
        delete l[i];
    }
    delete l;
    return 0;
}