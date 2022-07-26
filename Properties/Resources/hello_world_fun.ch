using minecraft;

namespace mydatapack {

	[minecraft:load]
	/*
		Multi-line Comment
	*/
	function hello_world_fun {
		// Comment
		as(@e[type = player])
			at (@s) {
				say("hello world");
				say("This is a SeaHopper test");
			}

		// scores don't need to be declared to function, but will cause errors when called and don't exist.
		score add scoreTypeA;
		score add scoreTypeB;
		
		
		scoreTypeA someUserName = 0; 
		scoreTypeB someUserName = 3;
		// Implicit use of "a @s = 0", will set variable a for @s instead of 'scoreboard'
		scoreTypeA = 20;
		
		as (@r)
			while(scoreTypeA someUserName < scoreTypeB someUserName) /* This is less a loop and more of a recursion */{
				say("Increment!");
				scoreTypeA someUserName += 1;
			}
	}


	function my_function {
		as (@s) say("Foo");
	}


	[callme]
	function my_other_function {
		my_function();
		as (@s) say("Bar");
	}
}
