# PATHFINDING PROJECT REPORT
A game which uses A star algorithm to find the shortest path.
----------
Link to the game : https://himabindugssn.github.io/AI-pathfinding-game/game/index.html

---
# Outline :

## Introduction
- The actual challenges faced by the Mars Rover relevant to the project
- Goals

## Plan & choices taken
- UI
- Programming Language

## What I could implement
- Implemented A star algorithm
- Optimization

## What I couldn’t implement

## High Level Diagram

## Low Level Diagram

## Further optimization ideas

## References

---

# Introduction:

([Link to the PPT of the project approach](https://docs.google.com/presentation/d/1JBN6Q5IRm_dXOVzq4_44IbXiIYAPFawiwVQf4Q75pgQ/edit?usp=sharing))

  

## The actual challenges faced by the Mars Rover relevant to the project:

-   The rover wheels might have a hard time grasping onto the loose-gravel ground. The wheels could spin in place before they gain tracking. Rover might :-
    

- Stop before reaching the target

-   May hit an obstacle behind( if it gets into some groove - created by movement of the rover itself)- as the pathfinding is done in forward or unvisited directions.

-   Moving safely from rock to rock or location to location is a major challenge because of the communication time delay between Earth and Mars, which is about 20 minutes on average. The drivers of rovers on Mars cannot instantly see what is happening to a rover and they cannot send quick commands to prevent the -   rover from running into a rock or falling off of a cliff.
    
-   Movement should be in such a way that minimum energy is utilized.
    

  

## Goals:

-   Get the path from source A to destination B
    
-   Get around obstacles in the way - keep some distance from them and also store the details of the recent obstacle i.e. the path, cost and obstacle location (Lot’s of data can’t be stored but storing recent details is feasible in the real world scenario).
    
-   Find the shortest possible path - to save energy
    
-   Find the path quickly to prevent any miscommunications
    

  

# Plan & choices taken:

-   Build a 3D game wherein the slopes and terrain add to the heuristic cost of the path. ( steeper the slope, more is the cost; rough, loose terrain would have more cost ).
    
-   Set a limit for the fuel available for the rover - and user has to navigate using arrow keys to help the rover reach the destination quickly before the fuel gets over.
    
-   Effect of wind direction over the rover - set up a simulation of dust storms.
    
-   Set up a few dynamic obstacles.
    
-   The algorithm must satisfy the above-mentioned goals.
    

## UI:

### Need:

-   Must provide features for developing a 3D game.
    
-   Tools to add terrain, wind and slopes as objects.
    

  

### Choices: Unity3D over three.js

-   Although both use WebGL, and are cross platform compatible and have features to build 3D games, Unity will handle the upgrade of what browsers can do and handle over time but three js might require rewriting some parts of code so I opted for Unity.
    

## Programming Language:

### Need:

-   Support object oriented programming.
    
-   Supported by the chosen game engine(Unity).
    

  

### Choices: C# over js

-   C# is an object oriented language while js is scripting language.I need to have classes for different game objects and hence used C#.
    
-   Most of the UI elements are provided by the Unity and I need not code so js isn’t required. The minimum features could be coded using C#.
    
-   C# has more cross-platform support than JavaScript.
    

  

# What I could implement:

###  Implemented A star algorithm
    

Heuristic used : Diagonal distance (uniform cost) - best in the scenario where 8 way movement is possible. The disadvantage is that the diagonal and non diagonal cost is the same sometimes which might lead to zigzag paths.

-   The search space here is a grid system - wherein the space is divided into small equal sized squares. The smaller the size, the better is the path but the time taken for calculating the cost of the path would increase.
    

![](https://lh5.googleusercontent.com/eVyNkoxte6MdnyhM7KtZwr2wHjD4ewcgNnSQAPtVwS9_f9J67kuPKwwA4h7pFKKJTUw-v4_rYsPdkbvKD475HT8yRNiOIF7voBFmHCIetnWtvbQyuEagHxzBf28HyVXgX51gArWN)

Fig : sphere (capsule object’s top view) and the location of it (the square which has maximum fraction of capsule would be approximately it’s location.

  

![](https://lh4.googleusercontent.com/38b6mIon7eQMxVUuVgDIrHo6DoSBdbpLcXJXbPtI_YjkkTSzoOpyjOApJAI8CTnvt_M1R5Q5oic5OI9lSWVLNycFT_GMgIqXnxGkk-zS2qcRvOV7LDy1d7viDao4_Xg5u266RplT)

Fig : red colored squares represent “unwalkable” squares.

  

![](https://lh4.googleusercontent.com/u_Sr5towY1YdpnUovrmxxaqx-X2hygFUx3hbF6RiOEEKiD1nesR8A39R56-sxDIVjTggs9OmgHne2X5jBIIqBIxn_-MNFBbYK11YieEIOimJV1PHsatfBw4AvNS9OUZPMQRGV84q)

Fig : Path - black colored path

  

## Optimization :


Pseudo code :

    Openset - nodes to be visited // list of nodes
    Closed set- nodes not visited // hashset of nodes
    openset.add(src node)

    while(openset is not empty):
    
	    cur= node with lowest f_cost in openset
	    openset.remove(cur)
	    closedset.add(cur)
	    if( cur==dest node)
		    Retrace path()
		    Return
	    
    Node in neighbours(cur):
    
	    if(node visited or node not walkable)
	    
	        Continue
	    
	    if(node.gcost less or node not in openset)
	    
	         Node.fcost=get f cost()
	    
	         Node.parent=cur
	    
	    If node not in openset:
	    
	         openset.add(cur)

  

This- node with lowest f_cost in openset- is the costliest step is the one highlighted above which takes O(n) time. That could be done using heaps, where the time complexity would be just O(1) to extract minimum element and O(log n) to insert an element.

  

### Used heaps to decrease the time taken to find the path when the search space is large.
    

50 x 50 grid size : using list data structure - 5 ms

100 x 100 gird size : using list data structure - 52 ms

100 x 100 grid size : using heap - 8 ms

Case 1:For a 50 x 50 grid using a list data structure : time taken is 5 ms

  

![](https://lh6.googleusercontent.com/AyS2XQdT0A0PNYIYTkP1-kCq061mcjKOIDjIvDLHArZED299MKv980qkp7r16EBtpRXCtBdJDH_zcxOL0u69DUkkPP9Y39kywJ6a8kBkIn3TW7DnXI5crwJdBbBXL224bFofky7C)

![](https://lh6.googleusercontent.com/7UELjtae3OIg5zQgvnVWJMGsLcv5epm5hvgeO5DpA118w2BWEjEYf84E0sfq35iYel0u9MJPRdIT1Q_w0IbQ5MjU1rbzJhHNdywFNfOvCJfzxG9mOj82qiWDtF-_Koa5OyCvJTB9)

![](https://lh3.googleusercontent.com/sAuormw5qsHLVshyP0mXgX_3Jneq2aXTeUw6arLMtUG9xD25_P0mrRrEESXFNzs7FOT5HGwrnoL4n1CMatv-TYZth2mY2TeJcc1LPVtk4ogh9lSGtSwQTozvuVi0fQqJnxaI1W2c)

Case 2 : time taken for search space 100 x 100 grids is 52 ms.

![](https://lh3.googleusercontent.com/S7Zj3t0fVTJQR0eVuE3mAu8zwtkxpQvE7aDslxCcqL3QHPDgs_grbdZjr0zoeD6q43G51jL6MeAJ0wJfQQeDJk_ARRtaZYdOzoAqkvlQ-cxTcy8bz0uCMwRMUw9-YCcY4u_WP2m3)

![](https://lh3.googleusercontent.com/sG2LHRkxVsYv9NEV_j-vP7kA_KYNfp8KA0s4P-8wr-6EXpbCR5mwHdT_btqRlISbpXcdo2_lI3ZQwH3txt53nZdgCmbrQdDOfMaHbeaeiDLPlZlNp0cWOVIoR_049dOkY8JnCEx_)

![](https://lh3.googleusercontent.com/h9El_tRMuQjKpM3Y6Eq_-ipodIbZ4pobr2cEHhqxNAoIHVP_zyFdeRsw9WssZUtLfB1DNczg2gpe-PMEsw6LvrzEomwbKAg7Djqk6hlJIk_JvSpl8b3K3vtixC-ZSVvxMKj3Noor)

![](https://lh4.googleusercontent.com/6qb4AD3wipprPXQ8sQUGLo6u8kjy88HQGAR90n0imFcE7UDWulz0Q0PqSE9M5lojQi23hqdOejHBbmDXmi9op-wB1roPpnIFjNRxHeh4zqPwtGsWakQv9-Pnt3Mr-9U4sr4PQjpq)

Case 3: Using heaps in a 100 x 100 grid search space - took 8 ms only.

![](https://lh3.googleusercontent.com/S7Zj3t0fVTJQR0eVuE3mAu8zwtkxpQvE7aDslxCcqL3QHPDgs_grbdZjr0zoeD6q43G51jL6MeAJ0wJfQQeDJk_ARRtaZYdOzoAqkvlQ-cxTcy8bz0uCMwRMUw9-YCcY4u_WP2m3)

![](https://lh3.googleusercontent.com/sG2LHRkxVsYv9NEV_j-vP7kA_KYNfp8KA0s4P-8wr-6EXpbCR5mwHdT_btqRlISbpXcdo2_lI3ZQwH3txt53nZdgCmbrQdDOfMaHbeaeiDLPlZlNp0cWOVIoR_049dOkY8JnCEx_)

![](https://lh3.googleusercontent.com/h9El_tRMuQjKpM3Y6Eq_-ipodIbZ4pobr2cEHhqxNAoIHVP_zyFdeRsw9WssZUtLfB1DNczg2gpe-PMEsw6LvrzEomwbKAg7Djqk6hlJIk_JvSpl8b3K3vtixC-ZSVvxMKj3Noor)

![](https://lh6.googleusercontent.com/_jC9o05WKtvXMcfcPtphvOB9HquSCav1-IjBgFY_DbOkqIRmxmw1nPO6vpMQ6OFmEInf_32D0EsOKx8NB9KPiuZ2ph-7X5-Lh6uULQriYsRXcZ5bSvgB6ILeOL9QYfEVO8oqzTcd)

###  Used cosine from vector product to reduce the zigzag paths.
    

![](https://lh3.googleusercontent.com/Q94WcwFYi0kP2AD4iJqRtD7yL8teAbC4ifW_5DjcNq5mRFnBMDdOuzB1aIRVljThYPEFdVQOJCWOaituOGRsMfwFEm2v3WgM2KThKhomOx-u0UFCRChLiKsy2kyQSjoMQ76N0glQ)![](https://lh6.googleusercontent.com/fvdE1zJfmN2lkOO0oJ9-Yjuzms2craxnyKA4fAlqAengWDstkdC1LeMH9oqoJ3VWgc1fYWiPvjb53onhqGlElybapdM3BDie9AH0Rmfi2KrKbIZuT_FQ0jqb2MbyHEAJYzsCgGyw)

Before using cross product After using cross product

  

![](https://lh6.googleusercontent.com/DZERccsI9CIe0XPzWuLiHxgFC0w7GJkmNvChoM_mLTKAGB-Cczi7A2V5RHuGCoNAOVYzOfHrA4gS2EhVAX5csCrIXmsPhLoiDpdCxJqzoQDlrKsBNdfVavfWiS3DWy5UyhHGBmUW)![](https://lh3.googleusercontent.com/0YKY6BRIT_D6_TAurdYpXOr8nnBtc5Ku7JjpyOnhLaXdD_70zSXjvTHEIuSb37sNgVK9GAR0pWXS5tlttoal7spxU9098SaX4VbkkW-Yc_bYvWowU-1-W25rpaGd7IkKMedmdDas)

![](https://lh4.googleusercontent.com/YpoisrnjL9DTIGNwmc5X0SW0xrj-A6EbhIYIWUijUn8fs-yKdrKrI_FTLf00uH4mdmTDMAy9f-Dfv5JrwM3GqxhwiQLdnB5RomnwyJ7n1-AxVy-yiZDaTPbbZVON-gVQ7cGRpWNN)

  

double distance_between(Node node_cur ,Node node_dest, Node node_src)

int dx_1=Mathf.Abs(node_cur.x-node_dest.x);

int dy_1=Mathf.Abs(node_cur.y-node_dest.y);

int dx_2= Mathf.Abs(node_src.x-node_dest.x);

int dy_2=Mathf.Abs(node_src.y-node_dest.y);

double tie= Mathf.Abs(dx_1*dy_2- dy_1*dx_2);//to break the tie

return Math.Abs(tie*(14*(Mathf.Max(dx_1,dy_1)+ 10*(Mathf.Abs(dx_1-dy_1)))));

  
  

“tie” factor would favour the path which aligns with the vector which connects source and destination (using cross product) when there are paths which have the same cost.

  

Example :

  

![](https://lh5.googleusercontent.com/rWO76cvkMSUCXlJwm10xlcNNN88cRKVZhNBTodR7kdfvlja5aqJc59t_A3fsdisjCpf_TloNcIwK7mvr1hZmBAqDKildZN07b_hT4zx5o9_jUDUpZIM52NfaOP7hNwKffv9Q2h2I)

(source- red, destination - blue)

The pink path would have incase only the diagonal cost is used. If tie is combined with the diagonal cost, we get path similar to the blue one. It will help us prefer paths that are along the straight line from the starting point to the goal i.e incase the path is far from the starting-goal point then the cross product would be even higher.

  

![](https://lh5.googleusercontent.com/Xdc6J-dWjrfAD4d_rmtF1CuGE0Q_gbupkFZ8u74M3I88IFv8skj_Kka_HIcKLR4ZIoHrbxq_v9-M35Q1tSdRpgLX32f9z3lMGucxlx--7azyG8qsVgfFIIHcJ_zbsxiP546KveBP)

If the point (1,1) is taken up instead of (1,0), then the path would be like the”pink” path as shown in the previous diagram)

tie shows the cross product if the two vectors - one from starting point to the target node and the other one from selected node to target/goal.

  
  

### Adjusted the node radius such that it doesn’t cause lag(too small node radius can cause it) and gives approximate path (smaller node radius can give better path). I chose node radius as 0.05 because if I decrease it further it causes lag.
    

Node radius = 0.5

![](https://lh4.googleusercontent.com/WblKaUq2T_k-FNNaBWcFH9GPp5c3xEmEPdcIFmfuE_v7fJv9I4LnPGqUvx6uB8S9Q23RWs2V7Q1NC14qjsEX8iU-ADMaNslLPrVVznP3pz3i9hloTeC9rT1oH5HvZ2flcn6tLndj)

![](https://lh4.googleusercontent.com/XuRXXUeZDsL8tnESlqpSVXLoTUkw5F2u9eHOi7lSosTCAJhhP_7QXoErsEwkNhI5AaSzRKdwEAGQNugDhesaUX55ukUYIuRIspB3uUhVDv8U642-d0RveTgevC-0d0zCWtmaNrEz)

  

Node radius = 0.2

![](https://lh6.googleusercontent.com/NajmiMrKJ9xMVpuKvSZJICSh4T5nQ1eyXmIvNdGc9aXGv5a834d2-4qX-GrEFxKnfxqFEfQfQhBQLfE_UGNpnV9qElx0gddnRS0kr8drbdKgTs6ZWvzeeJZ_uN2FTti9DZbjTkFV)

(The black spots are the areas which are a part of the path)

![](https://lh3.googleusercontent.com/gM4w1ZhC4mF5A8HEEnRJN7c2__Copc-XGcudjNaPjV6DNm7pzvEvBcJJhn7Ms-ba6QOmbcJL-0QDjZV9rSP1cfxvRBNXnBoFFVHyze6UlbCTcFJMaG5RxO8eZAO_E2KmOwHKcnS6)

  

Node radius = 0.05

![](https://lh4.googleusercontent.com/4oYtsZ2EDY0RPqGetJJ8v8HShYgdx2AKqQVrsKbqlIEA-6L0_2MV8EGGorNhlMlUoqZi0kO97zarGgpGJ025NTZmjDDKUhHUyq46BmFNu2jxNdqrsaKawxqKY1FsWbT43vaeHY0k)

![](https://lh5.googleusercontent.com/F3ym1qdPM-xNSbNePXqJaCFe0WpXq2xsvsJOh4_4gJlyqZ_WI2fsk0PfjXfja5Xf7HeKNc31ySWoVxKwGSwXCsOB8YKmN6aczVY86kXoFJKF_wtfc2W1Jw4QmzoDLdji-VX9ICzq)

  

-   Show multiple rovers from different paths reaching the destination path
    

![](https://lh5.googleusercontent.com/vS7afWXk-NHgqR2RiJsPt0bsH3QTUiuiX0hLaYH0ODsLmPCBQz-I17fZ8IdZCI1zSGXyQVqIQXNpDhtUuQeZEQvcPCG6SJl_pXNNR7Bp_Cp9snqc6qg2tHMy6iw2xMtxjxQS-Rco)

  

-   Set the rover at any location and allow it to reach the destination - in various scenarios(levels).
    
-   Move the rover while it’s moving towards destination(at run time) to allow user take the rover quickly towards the destination.
    
-   Show the path travelled by the rover.
    

![](https://lh4.googleusercontent.com/4JjXO6_JnZY8OXzzxC0qY1eWTWdujLfdkY9sNDumkFZ3w0qWA1j7kJ-CLlLUHqo1yHf-p2_5l3tfaIVkznA1IHG9Q938L8tUHlhKuLpTKT-zMyr5dvBXJ_gsTpLzOI8xu8ku8F8_)

# What I couldn’t implement:

-   Terrains, slopes in the game (3D search space).
    
-   Wind effect over the rover.
    
-   Stones - which could be climbed upon by the rover - causing a misallignment from the path defined by algorithm.
    
-   Fuel limit of the rover - the rover has to reach the destination before fuel gets over - and for that user could use the arrow keys to help the rover.
    
-   Multiple destination points.
    
-   Add obstacles at run time.
    
-   Allow the user to change the destination point.
    
-   Other algorithms provided in the sample project.
    

  

# High Level Diagram:

![](https://lh6.googleusercontent.com/WjYvhCSCL1KW0FiSA7hJRiO13zKp3wJOUOxXyuiIqHYcwfK_5VMSla4WyLyUmd_1qmpPejjLGUUm5HSvGOiIuiAR-qkh-iAqgGu1p3sEiyikZj6hSArYOXCq1lqglwtGWOaJVQWl)

# Low Level Diagram:

  

![](https://lh5.googleusercontent.com/osiDlKKELxxtA_nviWAJGhSvkWgD1AFYqWcGex8QSZXLagSPsnIV0y3zZBcQJu3kFz3sldzLF_0GwNbVcdYMDiHrj8Sg5fEyKc0UVghfkRLmrGazAH_s_-wrmJi6_X3FJ5tfNy9I)

# Further optimization ideas:

-   Use fibonacci heaps in place of binary heaps - the insertion time would be reduced from O(log n) to Θ(1) .
    
-   Use triangular map decomposition instead of simple grid search. The number of nodes (grids) would be less in triangular map as compared to a grid so it would increase the efficiency.
    

![](https://lh5.googleusercontent.com/eJTOVolwLg7XTLjVaSwUBmSivFCYR63nCBeMDlH2ZcdKXna7OesdzlURJLfSvnncKy4WthksuxI99zfO3lph2IhmBqkVujoytCAwcxR5Kpc8Gy9s9eG1_2FGLZBpqo7WOXSOpLTL)


# References :

[https://www.researchgate.net/profile/Xiao_Cui7/publication/267809499_A-based_Pathfinding_in_Modern_Computer_Games/links/54fd73740cf270426d125adc.pdf](https://www.researchgate.net/profile/Xiao_Cui7/publication/267809499_A-based_Pathfinding_in_Modern_Computer_Games/links/54fd73740cf270426d125adc.pdf)

[https://towardsdatascience.com/create-your-own-board-game-with-powerful-ai-from-scratch-part-1-5dcb028002b8](https://towardsdatascience.com/create-your-own-board-game-with-powerful-ai-from-scratch-part-1-5dcb028002b8)

[https://spaceplace.nasa.gov/mars-rovers/en/](https://spaceplace.nasa.gov/mars-rovers/en/)

[https://www.gamedev.net/tutorials/programming/artificial-intelligence/the-total-beginners-guide-to-game-ai-r4942/](https://www.gamedev.net/tutorials/programming/artificial-intelligence/the-total-beginners-guide-to-game-ai-r4942/)

[https://github.com/SebLague/Pathfinding](https://github.com/SebLague/Pathfinding)

[https://www.redblobgames.com/pathfinding/a-star/introduction.html](https://www.redblobgames.com/pathfinding/a-star/introduction.html)

[https://github.com/kylemcdonald/ofxPathfinder](https://github.com/kylemcdonald/ofxPathfinder)

[https://www.scirp.org/html/2-1730422_70460.htm#txtF0](https://www.scirp.org/html/2-1730422_70460.htm#txtF0)

[https://drops.dagstuhl.de/opus/volltexte/2013/4333/pdf/4.pdf](https://drops.dagstuhl.de/opus/volltexte/2013/4333/pdf/4.pdf)

[https://towardsdatascience.com/a-star-a-search-algorithm-eb495fb156bb](https://towardsdatascience.com/a-star-a-search-algorithm-eb495fb156bb)

[https://medium.com/@nicholas.w.swift/easy-a-star-pathfinding-7e6689c7f7b2](https://medium.com/@nicholas.w.swift/easy-a-star-pathfinding-7e6689c7f7b2)

[https://www.geeksforgeeks.org/a-search-algorithm/](https://www.geeksforgeeks.org/a-search-algorithm/)

[https://medium.com/ironequal/pathfinding-like-a-king-part-1-3013ea2c099](https://medium.com/ironequal/pathfinding-like-a-king-part-1-3013ea2c099)

[https://mars.nasa.gov/mer/mission/timeline/surfaceops/navigation/](https://mars.nasa.gov/mer/mission/timeline/surfaceops/navigation/)

[https://www.youtube.com/watch?v=LziIlLB2Kt4&t=128s](https://www.youtube.com/watch?v=LziIlLB2Kt4&t=128s)

[https://forum.unity.com/threads/how-to-sort-vertices-in-a-mesh.604150/](https://forum.unity.com/threads/how-to-sort-vertices-in-a-mesh.604150/)


