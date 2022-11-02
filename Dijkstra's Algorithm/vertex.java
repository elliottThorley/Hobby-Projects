import java.util.*;

public class vertex implements Comparator<vertex> {
  
    // Member variables of this class
    public int src;
    public float dist;
    public ArrayList<road> connections = new ArrayList<road>();
  
    public vertex() {}
  
    public vertex(int src, float dist,ArrayList<road> connections)
    {
        this.src = src;
        this.dist = dist;
        this.connections=connections;
    }
    public void setDist(float dist){
        this.dist=dist;
    }

    @Override public int compare(vertex vert1, vertex vert2)
    {
  
        if (vert1.dist < vert2.dist)
            return -1;
  
        if (vert1.dist > vert2.dist)
            return 1;
  
        return 0;
    }
}