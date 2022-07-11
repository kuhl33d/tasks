# include <iostream>
using namespace std;

class cnode
{
public:
    int info;
    cnode *pnext;
    
};

class clist
{
public:
    cnode *phead ;
    cnode *ptail ;

    public:
    clist()
    {
        phead = NULL;
        ptail = NULL;
    }
    
    void attach (cnode *pnn)
    {
        if (phead == NULL)
        {
            phead = pnn;
            ptail = pnn;
            
        }else
        {
            ptail -> pnext =pnn;
            ptail =pnn;
            
        }
    }
    
    void disp()
    {
        cnode *ptrav2;
        ptrav2 = phead;
        
        while (ptrav2 != NULL)
        {
            cout << ptrav2 -> info << ' ';
            ptrav2 = ptrav2 ->pnext;
        }
        cout << endl;
    }
  
};



void cut(int n ,int v ,clist &ml,clist &l3, clist &l4 )
{
    cnode *pnn = NULL;
    cnode *ptrav;
    cnode *ptrav1;
    
    
    l3.phead = ml.phead;
    ml.phead = NULL;
    ptrav = l3.phead;
    
    for ( int i =0 ;i<n  ; i++)
    {
        
        
        
        if (ptrav ->pnext-> info == v)
        {
            l4.phead = ptrav->pnext;
            l3.ptail = ptrav;
            l4.ptail = ml.ptail;
            ptrav->pnext=NULL;
            ml.ptail = NULL;
            break;
        }
        
        ptrav = ptrav -> pnext ;
    }
    
    
   
  
    
   
    
   
    
}


int main()
{
    clist l1;
    clist l2;
    clist l3;
    clist l4;
    clist ml;
    
    cnode *pnn;
    int n;
    cin>> n;
    
    int v;
    cin >>v;
    
    for (int i=0 ; i< n ; i++)
    {
        pnn = new cnode;
        cin>>pnn->info;
        pnn->pnext=NULL;
        ml.attach(pnn);
        
    }
    cut(n, v, ml, l3, l4);    
    l1.disp();
    l2.disp();
    // l3.disp();
    // l4.disp();
    
}


