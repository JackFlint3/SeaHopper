/* 
 * Implies all directly underlying names to the path 'minecraft' 
 * such as 'minecraft:bat' or 'minecraft:cow' to be just 'bat' or 'cow' respectively.
 * Will be resolved at compile-time and its scope is limited to the file
 */
using minecraft; 
// the 'as' Keyword here registers the statement as a constant for the document.
using (@e[type=bat] as bats);

// Defining a namespace of name 'mynamespace'
namespace mynamespace {

	// Defining a function for 'mynamespace' under the name of 'myfunction'
	function  myfunction {
		// a command out of 'minecraft' namespace.
		say("Hello World");

		// This 'minecraft:' already is resolved by the using directive
		minecraft:say("This also is valid");
		
		
		// defines a new scoreboard objective of the name 'a' and type 'dummy'
		score.define("a", dummy);

		// same as for 'a' except the overload implies the dummy type
		score.define("b");


		// the 'using' statement can also limit its scope to a body
		using (@p["playername"] as playername) {
			
			// here the static declarations of the using statement is being used to access a scoreboard
			playername.score["a"] = 5;
			
			// This type of assignment works as well.
			bats.score["a"] = playername.score["a"];
			
			// Infers a "if score" statement as scoreboard objectives are being compared
			if (playername.score["a"] <= playername.score["b"]) {
				playername.tag += "lowerAScore";
			}
		}

		// Entity selectors dont need to be wrapped in using statements to function
		if (@p["someOtherPlayerName"].score["a"] == 1){
			as @p["someOtherPlayerName"] 
				
				// Comparing block data
				if (@b[~,~-1,~] == minecraft:sand) {
					// Setting block data
					@b[~,~-1,~] = minecraft:dirt;
			}
		}

		// This checks for existence of a path on a resource location in storage
		if (namespace:resource/location.identifier == {nbt.path[2].check}) {
			
		} else {
			// Else statements esentially mark a 'unless' statement
		}

		// data get with nbt path
		namespace:resource/location.identifier{nbt.path[2].get};
		// data get with scale
		namespace:resource/location.identifier{nbt.path[2].get} * 10;

		// data merge
		namespace:resource/location.identifier += {nbt.path[2].get};

		// data modify append from block nbt
		namespace:resource/location.identifier{nbt.path[2].modify} += @b[~,~,~]{nbt.path}
		// data modify append index from player nbt
		namespace:resource/location.identifier{nbt.path[2].indexed[0]} += @p{nbt.path}



	}
}