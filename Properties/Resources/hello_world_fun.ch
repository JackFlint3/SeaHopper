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

		// scores don't need to be declared to function, but will cause errors when called and dont exist.
		score add a;
		score add b;
		
		
		a scoreboard = 0; 
		b scoreboard = 3;
		// Implicit use of "a @s = 0", will set variable a for @s instead of 'scoreboard'
		a = 20;
		
		as (@r)
			while(a scoreboard < b scoreboard) /* This is less a loop and more of a recursion */{
				say("Increment!");
				a scoreboard += 1;
			}
	}


	function dont_call_me {
		as (@s) say("Foo");
	}


	[callme]
	function call_me {
		dont_call_me();
		as (@s) say("Bar");
	}
}


