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
    void push(node *pnn){
        if(tail!=NULL){
            tail->nxt = pnn;
        }
        tail = pnn;
        if(head == NULL){
            head=pnn;
        }
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
void find(list *l1,list *l2){
    node *t1 = l1->head;
    node *t2 = l2->head;
    int i=0;
    while (t1 != NULL && t2 != NULL){
        if(t1->v == t2->v)
            cout << i << " , " << t1->v << endl;
        i++;
        t1 = t1->nxt;
        t2 = t2->nxt;
    }
}

int main(){
    list *l1 = new list();
    char a='y';
    node *pnn;
    cout << "list 1" << endl;
    while(a=='y'){
        pnn = new node;
        cin >> pnn->v;
        pnn->nxt = NULL;
        l1->push(pnn);
        cout << "another ? y/n ";
        cin >> a;
    }
    list *l2 = new list();
    a = 'y';
    cout << "list 2" << endl;
    while(a=='y'){
        pnn = new node;
        cin >> pnn->v;
        pnn->nxt = NULL;
        l2->push(pnn);
        cout << "another? y/n ";
        cin >> a;
    }
    find(l1,l2);
    return 0;
}