#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <omp.h>

#define YEL  "\x1B[33m"
#define MAG  "\x1B[35m"
#define WHT  "\x1B[37m"

void ShowMazeBackwards(const char *maze, int width, int height) {
   int x, y;
   for(y = 1; y < height; y++) {
      for(x = 0; x < width; x++) {
         switch(maze[y * width + width-1-x]) {
         case 1: printf(YEL"██"WHT);  break;
         case 2: printf(MAG"██"WHT);  break;
         default: printf("  ");  break;
         }
      }
      printf("\n");
   }
}

void ShowMaze(const char *maze, int width, int height) {
   int x, y;
   for(y = 0; y < height; y++) {
      for(x = 0; x < width; x++) {
         switch(maze[y * width + x]) {
         case 1: printf("██");  break;
         case 2: printf(MAG"██"WHT);  break;
         default: printf("  ");  break;
         }
      }
      printf("\n");
   }
}

/*  Carve the maze starting at x, y. */
void CarveMaze(char *maze, int width, int height, int x, int y) {
   int x1, y1;
   int x2, y2;
   int dx, dy;
   int dir, count;

   dir = rand() % 4;
   count = 0;
   while(count < 4) {
      //uncomment to see how maze is generated
      //ShowMaze(maze, width, height);
      dx = 0; dy = 0;
      switch(dir) {
      case 0:  dx = 1;  break;
      case 1:  dy = 1;  break;
      case 2:  dx = -1; break;
      default: dy = -1; break;
      }
      x1 = x + dx;
      y1 = y + dy;
      x2 = x1 + dx;
      y2 = y1 + dy;
      if(   x2 > 0 && x2 < width && y2 > 0 && y2 < height
         && maze[y1 * width + x1] == 1 && maze[y2 * width + x2] == 1) {
         maze[y1 * width + x1] = 0;
         maze[y2 * width + x2] = 0;
         x = x2; y = y2;
         dir = rand() % 4;
         count = 0;
      } else {
         dir = (dir + 1) % 4;
         count += 1;
      }
   }
}

/* Generate maze in matrix maze with size width, height. */
void GenerateMaze(char *maze, int width, int height) {
   int x, y;

   /* Initialize the maze. */
   for(x = 0; x < width * height; x++) {
      maze[x] = 1;
   }
   maze[1 * width + 1] = 0;

   /* Seed the random number generator. */
   srand(time(0));

   /* Carve the maze. */
   for(y = 1; y < height; y += 2) {
      for(x = 1; x < width; x += 2) {
         CarveMaze(maze, width, height, x, y);
      }
   }

   /* Set up the entry and exit. */
   maze[0 * width + 1] = 0;
   maze[(height - 1) * width + (width - 2)] = 0;
}

/* Solve the maze. */
void SolveMaze(char *maze, int width, int height) {
   int dir, count;
   int x, y;
   int dx, dy;
   int forward;

   /* Remove the entry and exit. */
   maze[0 * width + 1] = 1;
   maze[(height - 1) * width + (width - 2)] = 1;

   forward = 1;
   dir = 0;
   count = 0;
   x = 1;
   y = 1;
   while(x != width - 2 || y != height - 2) {
      //printf("x: %d\ny: %d\ndx: %d\ndy: %d\n\n",x,y,dx,dy);
      dx = 0; dy = 0;
      switch(dir) {
      case 0:  dx = 1;  break;
      case 1:  dy = 1;  break;
      case 2:  dx = -1; break;
      default: dy = -1; break;
      }
      if(   (forward  && maze[(y + dy) * width + (x + dx)] == 0)
         || (!forward && maze[(y + dy) * width + (x + dx)] == 2)) {
         maze[y * width + x] = forward ? 2 : 3;
         x += dx;
         y += dy;
         forward = 1;
         count = 0;
         dir = 0;
      } else {
         dir = (dir + 1) % 4;
         count += 1;
         if(count > 3) {
            forward = 0;
            count = 0;
         }
      }
   }
   /* Replace the entry and exit. */
   maze[(height - 2) * width + (width - 2)] = 2;
   maze[(height - 1) * width + (width - 2)] = 2;

   //make the entry and exit go thru the boarder
   maze[1]=2;
}

int main(int argc,char *argv[]) {
   int width, height, numberOfThreads, solve;
   char *maze;

   if(argc != 5) {
      printf("usage: maze <width> <height> <threads> <solved (0=yes 1=no)>\n");
      exit(EXIT_FAILURE);
   }

   width=atoi(argv[1]);
   height=atoi(argv[2]);
   numberOfThreads=atoi(argv[3]);
   solve=atoi(argv[4]);

   //ERROR CHECKING START
   //make sure the number of threads is possible
   int safe=0;
   while(safe==0){
     if((double)height/(double)numberOfThreads<8){
       safe=0;
       numberOfThreads=numberOfThreads-1;
     }
     else{
       safe=1;
     }
   }
   
   height=height/numberOfThreads;
   //make sure the height and width are numbers that the maze gen can do
   if(width%2==0){
    width=width+1;
   }
   if(height%2==0){
    height=height+1;
   }

   if(width+1%4!=0){
     width=width+2;
   }
   if(height+1%4!=0){
     height=height+2;
   }
   height=height*numberOfThreads;
   //ERROR CHECKING END

   maze=(char*)malloc(width * height * sizeof(char*));

   long totalTsec=0;
   long totalTusec=0;
   struct timeval tt;

   //run in parallel
   #pragma omp parallel num_threads(numberOfThreads)
   {
     //timing portion
     long tusec;
     int f;
     float e;

     char *tempMaze;

     /* Allocate the maze array. */
     tempMaze = (char*)malloc(width * height/numberOfThreads * sizeof(char*));

     //start the timing
     f = gettimeofday(&tt,0);
     tusec = tt.tv_usec;

     /* Generate and display the maze. */
     GenerateMaze(tempMaze, width, height/numberOfThreads);

     //stop the timing
     f = gettimeofday(&tt,0);
     tusec=tt.tv_usec-tusec;

     /* Solve the maze if requested. */
     if(solve == 0) {
       SolveMaze(tempMaze, width, height/numberOfThreads);
     }

     //ShowMaze(tempMaze,width,height/numberOfThreads);
     int ii;
     for( ii = 0; ii < omp_get_max_threads(); ii++){
         if(ii == omp_get_thread_num()){
           if(ii%2!=0)
             ShowMazeBackwards(tempMaze,width,height/numberOfThreads);
           else
             ShowMaze(tempMaze,width,height/numberOfThreads);

           //add the timings up
           totalTusec+=tusec;
         }
       #pragma omp barrier
     }
    free(tempMaze);
   }//end of prallel

   //output all useful info
   printf("Width: %d\n",width);
   printf("Height: %d\n",height);
   printf("Threads: %d\n",numberOfThreads);
   if(solve==0)
   printf(MAG"Solved: Yes\n"WHT);
   else
   printf(MAG"Solved: No\n"WHT);
   printf(YEL"Time: %ld microseconds\n"WHT,totalTusec);

   /* Clean up. */
   free(maze);
   exit(EXIT_SUCCESS);
   return 0;
}
