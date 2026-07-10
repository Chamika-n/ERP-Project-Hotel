const API_BASE = "/api/rooms";

const initialRooms = [
  {
    id: 1,
    number: "101",
    type: "Standard",
    floor: "1",
    capacity: 2,
    rate: 14500,
    status: "Available",
    condition: "Excellent",
    amenities: "Wi-Fi, Air Conditioning, TV",
    notes: "Near the reception area."
  },
  {
    id: 2,
    number: "102",
    type: "Standard",
    floor: "1",
    capacity: 2,
    rate: 14500,
    status: "Occupied",
    condition: "Good",
    amenities: "Wi-Fi, Air Conditioning, TV",
    notes: ""
  },
  {
    id: 3,
    number: "103",
    type: "Deluxe",
    floor: "1",
    capacity: 2,
    rate: 19500,
    status: "Reserved",
    condition: "Excellent",
    amenities: "Wi-Fi, Air Conditioning, Smart TV, Mini Bar",
    notes: "Late check-in expected."
  },
  {
    id: 4,
    number: "201",
    type: "Deluxe",
    floor: "2",
    capacity: 2,
    rate: 20500,
    status: "Cleaning",
    condition: "Good",
    amenities: "Wi-Fi, Air Conditioning, Smart TV, Balcony",
    notes: "Priority cleaning requested."
  },
  {
    id: 5,
    number: "202",
    type: "Suite",
    floor: "2",
    capacity: 3,
    rate: 36500,
    status: "Available",
    condition: "Excellent",
    amenities: "Wi-Fi, Air Conditioning, Smart TV, Mini Bar, Jacuzzi",
    notes: ""
  },
  {
    id: 6,
    number: "203",
    type: "Family",
    floor: "2",
    capacity: 4,
    rate: 31500,
    status: "Maintenance",
    condition: "Repair Required",
    amenities: "Wi-Fi, Air Conditioning, TV, Refrigerator",
    notes: "Bathroom tap replacement pending."
  },
  {
    id: 7,
    number: "301",
    type: "Suite",
    floor: "3",
    capacity: 3,
    rate: 38500,
    status: "Occupied",
    condition: "Excellent",
    amenities: "Wi-Fi, Air Conditioning, Smart TV, Mini Bar, Balcony",
    notes: ""
  },
  {
    id: 8,
    number: "302",
    type: "Deluxe",
    floor: "3",
    capacity: 2,
    rate: 22500,
    status: "Available",
    condition: "Good",
    amenities: "Wi-Fi, Air Conditioning, Smart TV",
    notes: ""
  },
  {
    id: 9,
    number: "303",
    type: "Family",
    floor: "3",
    capacity: 5,
    rate: 33500,
    status: "Reserved",
    condition: "Excellent",
    amenities: "Wi-Fi, Air Conditioning, Two TVs, Refrigerator",
    notes: "Baby cot requested."
  },
  {
    id: 10,
    number: "401",
    type: "Suite",
    floor: "4",
    capacity: 2,
    rate: 42500,
    status: "Cleaning",
    condition: "Needs Inspection",
    amenities: "Wi-Fi, Air Conditioning, Smart TV, Mini Bar, Jacuzzi",
    notes: "Supervisor inspection after cleaning."
  },
  {
    id: 11,
    number: "402",
    type: "Deluxe",
    floor: "4",
    capacity: 2,
    rate: 24500,
    status: "Available",
    condition: "Excellent",
    amenities: "Wi-Fi, Air Conditioning, Smart TV, Balcony",
    notes: ""
  },
  {
    id: 12,
    number: "403",
    type: "Family",
    floor: "4",
    capacity: 4,
    rate: 34500,
    status: "Occupied",
    condition: "Good",
    amenities: "Wi-Fi, Air Conditioning, TV, Refrigerator",
    notes: ""
  }
];

let rooms = [];
let currentView = "table";

const elements = {
  roomTableBody: document.getElementById("roomTableBody"),
  cardView: document.getElementById("cardView"),
  tableView: document.getElementById("tableView"),
  emptyState: document.getElementById("emptyState"),
  globalSearch: document.getElementById("globalSearch"),
  mobileSearch: document.getElementById("mobileSearch"),
  statusFilter: document.getElementById("statusFilter"),
  typeFilter: document.getElementById("typeFilter"),
  floorFilter: document.getElementById("floorFilter"),
  clearFilters: document.getElementById("clearFilters"),
  totalRooms: document.getElementById("totalRooms"),
  availableRooms: document.getElementById("availableRooms"),
  occupiedRooms: document.getElementById("occupiedRooms"),
  attentionRooms: document.getElementById("attentionRooms"),
  resultCount: document.getElementById("resultCount"),
  lastUpdated: document.getElementById("lastUpdated"),
  tableViewBtn: document.getElementById("tableViewBtn"),
  cardViewBtn: document.getElementById("cardViewBtn"),
  roomForm: document.getElementById("roomForm"),
  statusForm: document.getElementById("statusForm"),
  roomModalLabel: document.getElementById("roomModalLabel"),
  openAddRoom: document.getElementById("openAddRoom"),
  resetDataBtn: document.getElementById("resetDataBtn"),
  appToast: document.getElementById("appToast"),
  toastMessage: document.getElementById("toastMessage"),
  sidebar: document.getElementById("sidebar"),
  menuToggle: document.getElementById("menuToggle")
};

const roomModal = bootstrap.Modal.getOrCreateInstance(
  document.getElementById("roomModal")
);
const statusModal = bootstrap.Modal.getOrCreateInstance(
  document.getElementById("statusModal")
);
const appToast = bootstrap.Toast.getOrCreateInstance(elements.appToast, {
  delay: 2600
});

async function loadRooms() {
  try {
    const response = await fetch(API_BASE);
    if (!response.ok) throw new Error("Unable to load rooms");
    rooms = await response.json();
    render();
    updateTimestamp();
  } catch (error) {
    console.warn("Could not load room data:", error);
    rooms = structuredClone(initialRooms);
    render();
    updateTimestamp();
  }
}

async function saveRooms() {
  updateTimestamp();
}

function formatCurrency(amount) {
  return new Intl.NumberFormat("en-LK", {
    style: "currency",
    currency: "LKR",
    maximumFractionDigits: 0
  }).format(amount);
}

function escapeHtml(value) {
  return String(value ?? "")
    .replaceAll("&", "&amp;")
    .replaceAll("<", "&lt;")
    .replaceAll(">", "&gt;")
    .replaceAll('"', "&quot;")
    .replaceAll("'", "&#039;");
}

function slugify(value) {
  return String(value)
    .toLowerCase()
    .replaceAll(" ", "-");
}

function conditionClass(condition) {
  const map = {
    Excellent: "condition-excellent",
    Good: "condition-good",
    "Needs Inspection": "condition-inspection",
    "Repair Required": "condition-repair"
  };
  return map[condition] || "condition-good";
}

function getSearchTerm() {
  return (elements.globalSearch?.value || elements.mobileSearch?.value || "")
    .trim()
    .toLowerCase();
}

function filteredRooms() {
  const term = getSearchTerm();
  const status = elements.statusFilter.value;
  const type = elements.typeFilter.value;
  const floor = elements.floorFilter.value;

  return rooms
    .filter((room) => {
      const searchable = [
        room.number,
        room.type,
        room.floor,
        room.status,
        room.condition,
        room.amenities
      ]
        .join(" ")
        .toLowerCase();

      return !term || searchable.includes(term);
    })
    .filter((room) => status === "All" || room.status === status)
    .filter((room) => type === "All" || room.type === type)
    .filter((room) => floor === "All" || room.floor === floor)
    .sort((a, b) =>
      String(a.number).localeCompare(String(b.number), undefined, {
        numeric: true
      })
    );
}

function render() {
  const data = filteredRooms();
  renderTable(data);
  renderCards(data);
  renderStats();
  updateResultState(data.length);
}

function renderTable(data) {
  elements.roomTableBody.innerHTML = data
    .map(
      (room) => `
      <tr>
        <td>
          <div class="room-cell">
            <div class="room-door"><i class="bi bi-door-open"></i></div>
            <div>
              <strong>Room ${escapeHtml(room.number)}</strong>
              <small>ID: RM-${String(room.id).padStart(4, "0")}</small>
            </div>
          </div>
        </td>
        <td><span class="room-type">${escapeHtml(room.type)}</span></td>
        <td>Floor ${escapeHtml(room.floor)}</td>
        <td>
          <span class="capacity">
            <i class="bi bi-people"></i>
            ${escapeHtml(room.capacity)} guests
          </span>
        </td>
        <td><span class="rate">${formatCurrency(room.rate)}</span></td>
        <td>
          <button
            class="status-badge status-${slugify(room.status)} border-0"
            type="button"
            onclick="openStatusModal(${room.id})"
            title="Click to update status"
          >
            ${escapeHtml(room.status)}
          </button>
        </td>
        <td>
          <span class="condition-badge ${conditionClass(room.condition)}">
            ${escapeHtml(room.condition)}
          </span>
        </td>
        <td>
          <div class="action-group">
            <button
              class="action-btn"
              type="button"
              onclick="openStatusModal(${room.id})"
              title="Update status"
              aria-label="Update status for room ${escapeHtml(room.number)}"
            >
              <i class="bi bi-arrow-repeat"></i>
            </button>
            <button
              class="action-btn"
              type="button"
              onclick="editRoom(${room.id})"
              title="Edit room"
              aria-label="Edit room ${escapeHtml(room.number)}"
            >
              <i class="bi bi-pencil-square"></i>
            </button>
            <button
              class="action-btn delete"
              type="button"
              onclick="deleteRoom(${room.id})"
              title="Delete room"
              aria-label="Delete room ${escapeHtml(room.number)}"
            >
              <i class="bi bi-trash3"></i>
            </button>
          </div>
        </td>
      </tr>
    `
    )
    .join("");
}

function renderCards(data) {
  elements.cardView.innerHTML = data
    .map(
      (room) => `
      <article class="room-card">
        <div class="room-card-top">
          <div>
            <div class="room-card-number">Room ${escapeHtml(room.number)}</div>
            <span class="room-card-type">${escapeHtml(room.type)} · Floor ${escapeHtml(room.floor)}</span>
          </div>
          <span class="status-badge status-${slugify(room.status)}">
            ${escapeHtml(room.status)}
          </span>
        </div>

        <div class="room-card-details">
          <div class="room-card-detail">
            <span>Capacity</span>
            <strong><i class="bi bi-people"></i> ${escapeHtml(room.capacity)} guests</strong>
          </div>
          <div class="room-card-detail">
            <span>Nightly Rate</span>
            <strong>${formatCurrency(room.rate)}</strong>
          </div>
          <div class="room-card-detail">
            <span>Condition</span>
            <strong>${escapeHtml(room.condition)}</strong>
          </div>
          <div class="room-card-detail">
            <span>Amenities</span>
            <strong>${escapeHtml(room.amenities.split(",").slice(0, 2).join(", "))}</strong>
          </div>
        </div>

        <div class="room-card-actions">
          <button class="btn btn-light" type="button" onclick="openStatusModal(${room.id})">
            <i class="bi bi-arrow-repeat"></i> Status
          </button>
          <button class="btn btn-gold" type="button" onclick="editRoom(${room.id})">
            <i class="bi bi-pencil-square"></i> Edit
          </button>
        </div>
      </article>
    `
    )
    .join("");
}

function renderStats() {
  elements.totalRooms.textContent = rooms.length;
  elements.availableRooms.textContent = rooms.filter(
    (room) => room.status === "Available"
  ).length;
  elements.occupiedRooms.textContent = rooms.filter(
    (room) => room.status === "Occupied"
  ).length;
  elements.attentionRooms.textContent = rooms.filter((room) =>
    ["Cleaning", "Maintenance"].includes(room.status)
  ).length;
}

function updateResultState(count) {
  const hasResults = count > 0;

  elements.emptyState.classList.toggle("d-none", hasResults);
  elements.tableView.classList.toggle(
    "d-none",
    !hasResults || currentView !== "table"
  );
  elements.cardView.classList.toggle(
    "d-none",
    !hasResults || currentView !== "card"
  );
  elements.resultCount.textContent = `Showing ${count} of ${rooms.length} room${
    rooms.length === 1 ? "" : "s"
  }`;
}

function updateTimestamp() {
  const now = new Date();
  elements.lastUpdated.textContent = now.toLocaleTimeString([], {
    hour: "2-digit",
    minute: "2-digit"
  });
}

function showToast(message) {
  elements.toastMessage.textContent = message;
  appToast.show();
}

function resetRoomForm() {
  elements.roomForm.reset();
  elements.roomForm.classList.remove("was-validated");
  document.getElementById("roomId").value = "";
  document.getElementById("roomCapacity").value = "2";
  document.getElementById("roomStatus").value = "Available";
  document.getElementById("roomCondition").value = "Excellent";
  elements.roomModalLabel.textContent = "Add New Room";
}

function fillRoomForm(room) {
  document.getElementById("roomId").value = room.id;
  document.getElementById("roomNumber").value = room.number;
  document.getElementById("roomType").value = room.type;
  document.getElementById("roomFloor").value = room.floor;
  document.getElementById("roomCapacity").value = room.capacity;
  document.getElementById("roomRate").value = room.rate;
  document.getElementById("roomStatus").value = room.status;
  document.getElementById("roomCondition").value = room.condition;
  document.getElementById("roomAmenities").value = room.amenities;
  document.getElementById("roomNotes").value = room.notes;
}

function isRoomNumberUnique(number, currentId = null) {
  return !rooms.some(
    (room) =>
      room.number.toLowerCase() === number.toLowerCase() &&
      room.id !== currentId
  );
}

window.editRoom = function editRoom(id) {
  const room = rooms.find((item) => item.id === id);
  if (!room) return;

  resetRoomForm();
  elements.roomModalLabel.textContent = `Edit Room ${room.number}`;
  fillRoomForm(room);
  roomModal.show();
};

window.openStatusModal = function openStatusModal(id) {
  const room = rooms.find((item) => item.id === id);
  if (!room) return;

  document.getElementById("statusRoomId").value = room.id;
  document.getElementById("selectedRoomText").textContent =
    `Room ${room.number} · ${room.type}`;
  document.getElementById("quickStatus").value = room.status;
  statusModal.show();
};

window.deleteRoom = async function deleteRoom(id) {
  const room = rooms.find((item) => item.id === id);
  if (!room) return;

  const confirmed = window.confirm(
    `Delete Room ${room.number}? This action removes it from the server data.`
  );
  if (!confirmed) return;

  try {
    const response = await fetch(`${API_BASE}/${id}`, { method: "DELETE" });
    if (!response.ok) throw new Error("Delete failed");
    await loadRooms();
    showToast(`Room ${room.number} was deleted.`);
  } catch (error) {
    console.error(error);
    showToast("Unable to delete room right now.");
  }
};

elements.roomForm.addEventListener("submit", async (event) => {
  event.preventDefault();

  const form = event.currentTarget;
  const idValue = document.getElementById("roomId").value;
  const currentId = idValue ? Number(idValue) : null;
  const roomNumberInput = document.getElementById("roomNumber");
  const roomNumber = roomNumberInput.value.trim();

  roomNumberInput.setCustomValidity(
    isRoomNumberUnique(roomNumber, currentId)
      ? ""
      : "Room number already exists."
  );

  if (!form.checkValidity()) {
    form.classList.add("was-validated");
    return;
  }

  const roomData = {
    id: currentId ?? Date.now(),
    number: roomNumber,
    type: document.getElementById("roomType").value,
    floor: document.getElementById("roomFloor").value,
    capacity: Number(document.getElementById("roomCapacity").value),
    rate: Number(document.getElementById("roomRate").value),
    status: document.getElementById("roomStatus").value,
    condition: document.getElementById("roomCondition").value,
    amenities:
      document.getElementById("roomAmenities").value.trim() ||
      "Standard amenities",
    notes: document.getElementById("roomNotes").value.trim()
  };

  try {
    let response;
    if (currentId) {
      response = await fetch(`${API_BASE}/${currentId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(roomData)
      });
      if (!response.ok) throw new Error("Update failed");
      showToast(`Room ${roomData.number} was updated successfully.`);
    } else {
      response = await fetch(API_BASE, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(roomData)
      });
      if (!response.ok) throw new Error("Create failed");
      showToast(`Room ${roomData.number} was added successfully.`);
    }

    await loadRooms();
    roomModal.hide();
    resetRoomForm();
  } catch (error) {
    console.error(error);
    showToast("Unable to save room right now.");
  }
});

elements.statusForm.addEventListener("submit", async (event) => {
  event.preventDefault();

  const id = Number(document.getElementById("statusRoomId").value);
  const newStatus = document.getElementById("quickStatus").value;
  const room = rooms.find((item) => item.id === id);

  if (!room) return;

  try {
    const response = await fetch(`${API_BASE}/${id}`, {
      method: "PATCH",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ status: newStatus })
    });

    if (!response.ok) throw new Error("Status update failed");
    await loadRooms();
    statusModal.hide();
    showToast(`Room ${room.number} status changed to ${newStatus}.`);
  } catch (error) {
    console.error(error);
    showToast("Unable to change room status right now.");
  }
});

elements.openAddRoom.addEventListener("click", resetRoomForm);

elements.statusFilter.addEventListener("change", render);
elements.typeFilter.addEventListener("change", render);
elements.floorFilter.addEventListener("change", render);

[elements.globalSearch, elements.mobileSearch].forEach((input) => {
  input?.addEventListener("input", (event) => {
    const otherInput =
      event.currentTarget === elements.globalSearch
        ? elements.mobileSearch
        : elements.globalSearch;

    if (otherInput) {
      otherInput.value = event.currentTarget.value;
    }

    render();
  });
});

elements.clearFilters.addEventListener("click", () => {
  elements.statusFilter.value = "All";
  elements.typeFilter.value = "All";
  elements.floorFilter.value = "All";

  if (elements.globalSearch) elements.globalSearch.value = "";
  if (elements.mobileSearch) elements.mobileSearch.value = "";

  render();
});

elements.tableViewBtn.addEventListener("click", () => {
  currentView = "table";
  elements.tableViewBtn.classList.add("active");
  elements.cardViewBtn.classList.remove("active");
  render();
});

elements.cardViewBtn.addEventListener("click", () => {
  currentView = "card";
  elements.cardViewBtn.classList.add("active");
  elements.tableViewBtn.classList.remove("active");
  render();
});

elements.resetDataBtn.addEventListener("click", async () => {
  const confirmed = window.confirm(
    "Reset all room records to the original demo data?"
  );
  if (!confirmed) return;

  try {
    const response = await fetch(`${API_BASE}/reset`, { method: "POST" });
    if (!response.ok) throw new Error("Reset failed");
    await loadRooms();
    showToast("Demo room data was restored.");
  } catch (error) {
    console.error(error);
    showToast("Unable to reset room data right now.");
  }
});

let overlay = null;

function closeSidebar() {
  elements.sidebar.classList.remove("open");
  document.body.classList.remove("sidebar-lock");

  if (overlay) {
    overlay.remove();
    overlay = null;
  }
}

elements.menuToggle.addEventListener("click", () => {
  const opening = !elements.sidebar.classList.contains("open");

  if (opening) {
    elements.sidebar.classList.add("open");
    document.body.classList.add("sidebar-lock");

    overlay = document.createElement("div");
    overlay.className = "sidebar-overlay";
    overlay.addEventListener("click", closeSidebar);
    document.body.appendChild(overlay);
  } else {
    closeSidebar();
  }
});

window.addEventListener("resize", () => {
  if (window.innerWidth >= 992) {
    closeSidebar();
  }
});

document.getElementById("roomModal").addEventListener("hidden.bs.modal", () => {
  resetRoomForm();
  document.getElementById("roomNumber").setCustomValidity("");
});

updateTimestamp();
loadRooms();

window.addEventListener("focus", () => {
  loadRooms();
});

setInterval(() => {
  if (document.visibilityState === "visible") {
    loadRooms();
  }
}, 5000);
