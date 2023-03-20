# Detect the operating system
ifeq ($(shell uname), Darwin)
    # macOS build instructions
	FLAGS = mono
else ifeq ($(OS), Windows_NT)
    # Windows build instructions
	FLAGS = 
else
    $(error Unsupported operating system)
endif

# Build instructions
bfs:
	csc /out:bin/BFS.exe src/bfs.cs src/utility.cs src/Tile.cs

dfs:
	csc /out:bin/DFS.exe src/dfs.cs src/utility.cs src/Tile.cs

tsp:
	csc /out:bin/TSP.exe src/tsp.cs src/utility.cs src/Tile.cs

main:
	csc /out:bin/Main.exe src/Main.cs src/utility.cs src/Tile.cs

run:
	$(FLAGS) bin/*.exe

clean:
	rm bin/*.exe

