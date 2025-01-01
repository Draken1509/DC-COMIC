
	// filter A-Z
	function sortCharacters(order) {
		console.log(`Sorting by ${order}`);
		// Add your sorting logic here
}

    // sidebar
	document.addEventListener("DOMContentLoaded", function () {
		// Select header elements for the alignment and type filters
		const alignmentHeader = document.querySelector('[href="#alignmentFilters"]');
		const typeHeader = document.querySelector('[href="#typeFilters"]');

		// Initialize icon state for collapsed filters
		document.querySelectorAll(".toggle-icon").forEach(icon => {
			icon.textContent = "+";  // Set initial icon state to "+"
		});

		// Add event listeners for Bootstrap's collapse events
		document.getElementById("alignmentFilters").addEventListener("shown.bs.collapse", function () {
			alignmentHeader.querySelector(".toggle-icon").textContent = "-";
		});

		document.getElementById("alignmentFilters").addEventListener("hidden.bs.collapse", function () {
			alignmentHeader.querySelector(".toggle-icon").textContent = "+";
		});

		document.getElementById("typeFilters").addEventListener("shown.bs.collapse", function () {
			typeHeader.querySelector(".toggle-icon").textContent = "-";
		});

		document.getElementById("typeFilters").addEventListener("hidden.bs.collapse", function () {
			typeHeader.querySelector(".toggle-icon").textContent = "+";
		});
	});



	// Character data with alignment and type for filtering
	const characters = [
	  { name: "Batman", alignment: "Heroes", type: "Individuals" },
	  { name: "Batwoman", alignment: "Heroes", type: "Individuals" },
	  { name: "Beast Boy", alignment: "Heroes", type: "Individuals" },
	  { name: "Big Barda", alignment: "Heroes", type: "Individuals" },
	  { name: "Aquaman", alignment: "Heroes", type: "Individuals" },
	  { name: "Alan Scott", alignment: "Heroes", type: "Individuals" },
	  { name: "Amanda Waller", alignment: "Villains", type: "Individuals" },
	  { name: "Brainiac", alignment: "Villains", type: "Individuals" },
	  { name: "Anarky", alignment: "Villains", type: "Individuals" },
	  { name: "Blue Beetle", alignment: "Heroes", type: "Individuals" },
	  { name: "Booster Gold", alignment: "Heroes", type: "Individuals" },
	  { name: "Anti-Monitor", alignment: "Villains", type: "Individuals" },
	  { name: "Antiope", alignment: "Heroes", type: "Individuals" },
	  { name: "Ares", alignment: "Villains", type: "Individuals" },
	  { name: "Artemis", alignment: "Heroes", type: "Individuals" },
	  { name: "Atom", alignment: "Heroes", type: "Individuals" },
	  // Add more characters if needed
	];

	const charactersPerPage = 8;
	let currentPage = 1;

	function displayCharacters() {
	  const start = (currentPage - 1) * charactersPerPage;
	  const end = start + charactersPerPage;
	  const filteredCharacters = filterCharacters();
	  const characterContainer = document.getElementById("characterContainer");

	  characterContainer.innerHTML = "";
	  filteredCharacters.slice(start, end).forEach(character => {
		const col = document.createElement("div");
		col.className = "col";
		col.innerHTML = `<div class="character-card">
							<img src="https://via.placeholder.com/150" alt="${character.name}">
							<h5>${character.name}</h5>
						  </div>`;
		characterContainer.appendChild(col);
	  });
	}

	function filterCharacters() {
	  const searchKeyword = document.getElementById("searchKeyword").value.toLowerCase();
	  const selectedAlignments = Array.from(document.querySelectorAll("#alignmentFilters .filter-checkbox:checked")).map(cb => cb.value);
	  const selectedTypes = Array.from(document.querySelectorAll("#typeFilters .filter-checkbox:checked")).map(cb => cb.value);

	  return characters.filter(character => {
		const matchesKeyword = character.name.toLowerCase().includes(searchKeyword);
		const matchesAlignment = selectedAlignments.length === 0 || selectedAlignments.includes(character.alignment);
		const matchesType = selectedTypes.length === 0 || selectedTypes.includes(character.type);

		return matchesKeyword && matchesAlignment && matchesType;
	  });
	}

	function updatePagination() {
	  const totalPages = Math.ceil(filterCharacters().length / charactersPerPage);
	  const paginationContainer = document.getElementById("paginationContainer");

	  paginationContainer.innerHTML = "";
	  for (let i = 1; i <= totalPages; i++) {
		const pageItem = document.createElement("li");
		pageItem.className = `page-item ${i === currentPage ? "active" : ""}`;
		pageItem.innerHTML = `<a class="page-link" href="#">${i}</a>`;
		pageItem.addEventListener("click", function (e) {
		  e.preventDefault();
		  currentPage = i;
		  displayCharacters();
		  updatePagination();
		});
		paginationContainer.appendChild(pageItem);
	  }
	}

	document.addEventListener("DOMContentLoaded", function () {
	  displayCharacters();
	  updatePagination();

	  // Filter listeners
	  document.querySelectorAll(".filter-checkbox, #searchKeyword").forEach(input => {
		input.addEventListener("input", () => {
		  currentPage = 1;
		  displayCharacters();
		  updatePagination();
		});
	  });
	});
