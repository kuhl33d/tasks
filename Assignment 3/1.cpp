//1.cpp
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
void remove_equal(list *l1,list *l2){
    node *t1 = l1->head,*t2 = l2->head,*prev=NULL,*nxt;
    while(t1!=NULL){
        nxt = t2->nxt;
        if(t1->v == t2->v){
            prev->nxt = t2->nxt;
            delete t2;
        }else{
            prev = t2;
        }
        t1 = t1->nxt,t2 = nxt;
    }
}
int main(){
    list *l1 = new list,*l2 = new list;
    node *pnn;
    int n;
    cout << "enter number of nodes: ";
    cin >> n;
    cout << "list 1" << endl;
    for(int i=0;i<n;i++){
        pnn = new node;
        cin >> pnn->v;
        pnn->nxt = NULL;
        l1->attach(pnn);
    }
    cout << "list 2" << endl;
    for(int i=0;i<n;i++){
        pnn = new node;
        cin >> pnn->v;
        pnn->nxt = NULL;
        l2->attach(pnn);
    }
    remove_equal(l1,l2);
    cout << "list 2: ";
    l2->disp();
    delete l1;
    delete l2;
    return 0;
}