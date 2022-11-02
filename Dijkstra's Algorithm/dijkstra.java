import java.io.*;
import java.util.*;

public class dijkstra{
    ArrayList<ArrayList<road> > adj = new ArrayList<ArrayList<road> >();
    ArrayList<vertex> vertz = new ArrayList<vertex>();
    Stack<Integer> mapStack = new Stack<Integer>();
    float totalMiles=0;
    public void dijkstra(ArrayList<ArrayList<road> > adj, int src,int dst,parse parsey){
        Set<Integer> settled = new HashSet<Integer>();
        PriorityQueue<vertex> pq=new PriorityQueue<vertex>(new vertex());
        this.adj = adj;
  
        for (int i = 0; i < adj.size(); i++){
            if(adj.get(i).size()!=0){
                vertex vert = new vertex(i,Integer.MAX_VALUE,adj.get(i));
                if(vertz.size()<=i){
                    for(int ii=vertz.size();ii<i;ii++){
                    vertz.add(null);
                    }
                    vertz.add(vert);
                }
                pq.add(vert);
            }
        }


        // Add source node to the priority queue
        vertex item = new vertex(src,0,adj.get(src));
        pq.add(item);
        int[] map = new int[adj.size()];
        float[] dists = new float[adj.size()];
        String[] signs = new String[adj.size()];
        //so if i print here, when we first make map, u think it will = 0
        //System.out.println(map[4]);

        while (!pq.isEmpty()) {
  
            // Terminating ondition check when
            // the priority queue is empty, return
            if (pq.isEmpty())
                return;
  
            // Removing the minimum distance node
            // from the priority queue
            vertex v = pq.remove();
  
            ArrayList<road> connections = v.connections;
            settled.add(v.src);
            //System.out.println(v.src);
            // All the neighbors of v
            for (int i = 0; i < connections.size(); i++) {
                if(settled.contains(connections.get(i).id)==true){
                    continue;
                }
                else{
                    vertex destination=vertz.get(connections.get(i).id);
                    float newDistance=connections.get(i).dist+v.dist;

                    if(newDistance<destination.dist){
                        destination.setDist(newDistance);
                        map[destination.src]=v.src;
                        dists[destination.src]=connections.get(i).dist;
                        signs[destination.src]=connections.get(i).signs;
                        pq.add(destination);
                    }
                }
            }//end of connection for loop
        }//ed of vertex for loop

        int k=dst;
        mapStack.push(dst);
        while(k!=0){
            //put into stack
            mapStack.push(map[k]);
            //totalMiles+=mapStack.peek().dist;
            //System.out.println("TotalMiles: "+totalMiles+"\n");
            k=map[k];
            //add the distance to the total miles

            if(k==0)
                break;
        }
        mapStack.pop();
        int count=1;
        while(mapStack.size()>1){
            System.out.print("        "+count+": ");
            count++;
            int temp=mapStack.pop();
            if(mapStack.peek()!=null)
                parsey.getInfo(temp,mapStack.peek(),dists,signs);
        }
        if(count==1)
        System.out.println("Roads can not connect, ensure that there is a continuous route between inputs");
    }
}