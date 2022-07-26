using minecraft;

namespace mydatapack {

	[minecraft:tick]
	function kill_bats {
		/*
			A function to kill all bats as soon as they spawn
		*/
		as(@e[type = bat, tag =! dying]) {
			say("I was a Bat");
			kill(@s);
			tag @s += dying;
		}
	}
}