#include<iostream>
using namespace std;
class Cnode{
    public:
    int info;
    Cnode *pnext;
};
class Clist{
    public:
    Cnode *head;
    Cnode *tail;
    Clist(){
        head=NULL;
        tail=NULL;
    }
    void attach(Cnode *pnn){
        if(tail!=NULL){
            tail->pnext = pnn;
        }
        tail = pnn;
        if(head == NULL){
            head=pnn;
        }
    }
    ~Clist(){
        Cnode *tmp;
        while(head!=NULL){
            tmp = head->pnext;
            delete head;
            head = tmp;
        }
    }
};
void find(Clist *l1,Clist *l2){
    Cnode *trav1 = l1->head;
    Cnode *trav2 = l2->head;
    int i=0;
    while (trav1 != NULL){
        if(trav1->info == trav2->info)
            cout << '[' << i << " , " << trav1->info << ']' << endl;
        i++;
        trav1 = trav1->pnext;
        trav2 = trav2->pnext;
    }
}

int main(){
    Clist *l1 = new Clist(),*l2 = new Clist();
    Cnode *pnn;
    int n;
    cout << "list 1 size: ";
    cin >> n;
    for(int i=0;i<n;i++){
        pnn = new Cnode;
        cin >> pnn->info;
        pnn->pnext = NULL;
        l1->attach(pnn);
    }
    cout << "list 2 size: ";
    cin >> n;
    for(int i=0;i<n;i++){
        pnn = new Cnode;
        cin >> pnn->info;
        pnn->pnext = NULL;
        l2->attach(pnn);
    }
    find(l1,l2);
    return 0;
}