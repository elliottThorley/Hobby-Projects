import java.util.*;

public class road implements Comparator<road> {
  
    // Member variables of this class
    public int id;
    public float dist;
    public String signs;
  
    public road() {}
  
    public road(int id, float dist,String signs)
    {
        this.id = id;
        this.dist = dist;
        this.signs=signs;
    }

    @Override public int compare(road road1, road road2)
    {
  
        if (road1.dist < road2.dist)
            return -1;
  
        if (road1.dist > road2.dist)
            return 1;
  
        return 0;
    }
    public String toString(){
    	String answer="id: ";
   		return(answer+id);
    }
}