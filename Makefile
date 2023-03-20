# Detect the operating system
ifeq ($(shell uname), Darwin)
    # macOS build instructions
	FLAGS = mono 
	DEL = rm
	SLASH = /
else
	FLAGS =	
	DEL = del
	SLASH = \\
endif

# Build instructions
bfs:
	csc /out:bin/BFS.exe src/Bfs.cs src/Utility.cs src/Tile.cs
	$(FLAGS) ./bin/BFS.exe

dfs:
	csc /out:bin/DFS.exe src/Dfs.cs src/Utility.cs src/Tile.cs
	$(FLAGS) ./bin/DFS.exe

tsp:
	csc /out:bin/TSP.exe src/Tsp.cs src/Bfs.cs src/Utility.cs src/Tile.cs
	$(FLAGS) ./bin/TSP.exe

main:
	csc /out:bin/Main.exe src/Main.cs src/Bfs.cs src/Dfs.cs src/Tsp.cs src/Utility.cs src/Tile.cs

run:
	$(FLAGS) ./bin/Main.exe

clean:
	$(DEL) bin$(SLASH)*.exe



