#include<iostream>
#include<string>
using namespace std;
// class ch{
//     public:
//         char v;
//         ch *nxt; 
// };
// class list{
//     public:
//     ch *head;
//     ch *tail;
//     list(){
//         head=NULL;
//         tail=NULL;
//     }
//     list(list *parent){
//         ch *pnn,*trav=parent->head;
//         while(trav != NULL){
//             pnn = new ch;
//             pnn->nxt = NULL;
//             pnn->v = trav->v;
//             this->attach(pnn);
//             trav=trav->nxt;
//         }
//     }
//     ~list(){
//         ch *tmp;
//         while(head != NULL){
//             tmp = head->nxt;
//             delete head;
//             head=tmp;
//         }
//     }
//     void attach(ch *pnn){
//         if(tail!=NULL)
//             tail->nxt=pnn;
//         tail=pnn;
//         if(head==NULL)
//             head=pnn;
//     }
//     void disp(){
//         ch *trav = head;
//         while(trav!=NULL){
//             cout << trav->v << " ";
//             trav=trav->nxt;
//         }
//         cout << endl;
//     }
// };
class node{
    public:
    int thieves[3];
    int bags[3];
    int money[3];
    int shore;
    node* next;
    string sol;
    node(){
        sol = "";
        shore=0;
        for(int i=0;i<3;i++){
            thieves[i] = 0;
            bags[i] = 0;
        }
        money[0] = 1000;
        money[1] = 700;
        money[2] = 300;
        next=NULL;
    }
    node(node *parent){
        this->sol = parent->sol;
        this->shore=parent->shore;
        for(int i=0;i<3;i++){
            this->thieves[i] = parent->thieves[i];
            this->bags[i] = parent->bags[i];
        }
        this->money[0] = 1000;
        this->money[1] = 700;
        this->money[2] = 300;
        this->next = NULL;
    }
    int isLegal(){
        int accum;
        for(int i=0;i<2;i++){
            accum=0;
            for(int j=0;j<3;j++){
                if(thieves[j] == i)
                    accum += money[j];
                if(bags[j] == i)
                    accum-=money[j];
            }
            if(accum < 0)
                return 0;
        }
        return 1;
    }
    int isEnd(){
        for(int i=0;i<3;i++){
            if(thieves[i] != 1 && bags[i] != 1)
                return 0;
        }
        return 1;
    }
    void disp(){
        cout << " shore: " << this->shore << endl;
        cout << " thieves: " ;
        for(int i=0;i<3;i++)
            cout << this->thieves[i] << " ";
        cout << endl;
        cout << " bags: ";
        for(int i=0;i<3;i++)
            cout << this->bags[i] << " ";
        cout << endl;
    }
};
class stack{
    public:
        int size;
        node *head;
        stack(){
            size=0;
            head=NULL;
        }
        int push(node *pnn){
            if(pnn->isLegal()){
                cout << " push" << endl;
                size++;
                pnn->next=head;
                head=pnn;
                return 1;
            }
            delete pnn;
            return 0;
        }
        node *pop(){
            size--;
            node *pnn = head;
            head = pnn->next;
            return pnn;
        }
        int found(node* search){
            if(search->shore == 0){
                if(search->thieves[0] == 0 && search->thieves[1] == 0 && search->thieves[2] == 0 
                && search->bags[0] == 0 && search->bags[1] == 0 && search->bags[2] == 0){
                    return 1;
                }
            }
            node* trav = head;
            while(trav != NULL){
                if(search->shore == trav->shore){
                    if(trav->thieves[0]==search->thieves[0] && trav->thieves[1]==search->thieves[1] && trav->thieves[2]==search->thieves[2] 
                    && trav->bags[0]==search->bags[0] && trav->bags[1]==search->bags[1] && trav->bags[2]==search->bags[2]){
                        return 1;
                    }
                }
                trav = trav->next;
            }
            return 0;
        }
        void disp(){
            node *t = head;
            while(t != NULL){
                t->disp();
                t = t->next;
            }
        }
};
void expand(node *pcurr,stack *S,stack *sol){
    node *child;
    int dbg;
    for(int i=0;i<3;i++){
        if(pcurr->thieves[i] == pcurr->shore){
            for(int j=i+1;j<3;j++){//thieve thieve
                if(pcurr->thieves[j] == pcurr->shore){
                    child = new node(pcurr);
                    (child->shore == 0)?child->shore=1:child->shore=0;
                    child->thieves[i]=child->shore,child->thieves[j]=child->shore;
                    if(child->isEnd()){
                        sol->push(child);
                    }else{
                        S->push(child);
                    }
                }
            }
            for(int j=0;j<3;j++){//thieve bag
                if(pcurr->bags[j] == pcurr->shore){
                    child = new node(pcurr);
                    (child->shore == 0)?child->shore=1:child->shore=0;
                    child->thieves[i]=child->shore,child->bags[j]=child->shore;
                    if(child->isEnd()){
                        sol->push(child);
                    }else{
                        S->push(child);
                    }
                }
            }
            // child = new node(pcurr);
            // (child->shore == 0)?child->shore=1:child->shore=0;
            // child->thieves[i]=child->shore;
            // if(child->isEnd()){
            //     sol->push(child);
            // }else{
            //     S->push(child);
            // }
        }
    }
    getchar();
    // S->disp();
}
void solve(stack *S,stack *sol){
    node *pnn,*pcurr;
    pnn = new node;
    S->push(pnn);
    while(S->head != NULL){
        pcurr = S->pop();
        expand(pcurr,S,sol);
        delete pcurr;
    }
}
int main(){
    stack *S = new stack,*sol = new stack,*mem = new stack;
    solve(S,sol);
    cout << sol->size << " solutions" << endl;
    delete S;
    delete sol;
}