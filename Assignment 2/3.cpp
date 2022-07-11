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
    void rotate(int index){
        int i=0;
        node *trav = head;
        while(trav!=NULL){
            if(i==index-1)
                break;
            trav=trav->nxt;
            i++;
        }
        cout << "trav " << trav->v << endl;;
        tail->nxt = head;
        head = trav->nxt;
        trav->nxt = NULL;
        tail=trav;
    }
};
int main(){
    list *l=new list;
    node *pnn;
    int n,index;
    cin >> n;
    for (int i=0;i<n;i++){
        pnn=new node;
        cin >> pnn->v;
        pnn->nxt=NULL;
        l->attach(pnn);
    }
    cout << "List: ";
    l->disp();
    cout << "node: ";
    cin >> index;
    l->rotate(index);
    cout << "List: ";
    l->disp();
    return 0;
}