# Detect the operating system
ifeq ($(shell uname), Darwin)
    # macOS build instructions
	FLAGS = mono
else
    # Windows build instructions
	FLAGS = 
endif

# Build instructions
bfs:
	csc /out:bin/BFS.exe src/Bfs.cs src/Utility.cs src/Tile.cs

dfs:
	csc /out:bin/DFS.exe src/dfs.cs src/Utility.cs src/Tile.cs

tsp:
	csc /out:bin/TSP.exe src/tsp.cs src/Bfs.cs src/Utility.cs src/Tile.cs

main:
	csc /out:bin/Main.exe src/Main.cs src/Bfs.cs src/dfs.cs src/tsp.cs src/Utility.cs src/Tile.cs

run:
	$(FLAGS) ./bin/DFS.exe

clean:
	rm bin/*.exe

