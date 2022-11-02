import java.io.*;
import java.util.*;


public class A5{
    public static void main(String[] args) throws FileNotFoundException{
        Scanner scan = new Scanner(System.in);

        System.out.println("Enter the Source Name: ");
        String sourceName = scan.nextLine();
        System.out.println("Enter the Destination Name: ");
        String destinationName = scan.nextLine();

        parse parser = new parse(sourceName,destinationName);
    }
}