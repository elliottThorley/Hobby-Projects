import java.io.*;
import java.util.*;

public class parse{
    public ArrayList<String> names = new ArrayList<String>();
    float totalMiles=0;
    public String sName;
    public String dName;
    public parse(String sourceName,String destName) throws FileNotFoundException{
        sName=sourceName;
        dName=destName;
        File file = new File("Road.txt");
        Scanner sc = new Scanner(file);

        //make the list
        //verticies
        int V = 0;
        road roads = new road();
        ArrayList<ArrayList<road> > adj = new ArrayList<ArrayList<road> >();

        while (sc.hasNextLine()){
            V++;
            //get line and split it
            String temp=sc.nextLine();
            String[] splitTemp=temp.split(",",4);

            //fill the variables
            int ID1=Integer.parseInt(splitTemp[0]);
            int ID2=Integer.parseInt(splitTemp[1]);
            float length=Float.parseFloat(splitTemp[2]);
            String signs=splitTemp[3];

            //make adjacency list out of roads
            if(adj.size()<=ID1){
                for(int i=adj.size();i<=ID1;i++){
                    adj.add(i,new ArrayList<road>());
                }
            }

            //add the actual road
            road tempRoad = new road(ID2,length,signs);
            //having ID1 in the get ENSURES that it is in the right spot
            //it would crash if this is not true
            adj.get(ID1).add(tempRoad);

            if(adj.size()<=ID2){
                for(int i=adj.size();i<=ID2;i++){
                    adj.add(new ArrayList<road>());
                }
            }
            tempRoad = new road(ID1,length,signs);
            adj.get(ID2).add(tempRoad);


        }//end of while loop

        //make the list of place names in arraylist
        File file2 = new File("Place.txt");
        Scanner sc2 = new Scanner(file2);
        
        while (sc2.hasNextLine()){
            String temp=sc2.nextLine();
            String[] splitTemp=temp.split(",",2);
            if(names.size()<=Integer.parseInt(splitTemp[0])){
                for(int i=names.size();i<=Integer.parseInt(splitTemp[0]);i++){
                    names.add(i,"");
                }
            }
            names.add(Integer.parseInt(splitTemp[0]),splitTemp[1]);
        }

        int userSRC=names.lastIndexOf(sourceName);
        int userDST=names.lastIndexOf(destName);

        System.out.println("Searching from: "+userSRC+"("+sourceName+")"+" to "+userDST+"("+destName+")");

        dijkstra dij = new dijkstra();
        dij.dijkstra(adj,userSRC,userDST,this);
    }
    public void getInfo(int id,int id2,float[] dists,String[] signs){
        totalMiles+=dists[id2];
        if(names.get(id).compareTo("")==0)
            System.out.println(id+"(null) -> "+id2+"("+names.get(id2)+")"+" , "+signs[id2]+" , "+dists[id2]+"mi.");

        else
            System.out.println(id+"("+names.get(id)+") -> "+id2+"("+names.get(id2)+")"+" , "+signs[id2]+" , "+dists[id2]+"mi.");

        if(names.get(id2).compareTo(dName)==0)
        System.out.println("It takes "+totalMiles+" from "+sName+" to "+dName);
    }
}