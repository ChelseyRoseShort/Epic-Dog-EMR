#!/usr/bin/env bash
set -euo pipefail

# Define project paths relative to the solution root
BACKEND="./EpicPetEMR/EpicPetEMR.csproj"
SHARED="./EpicPetEMR.Shared/EpicPetEMR.Shared.csproj"
UI="./EpicPetEMR-Ui/EpicPetEMR-Ui.csproj"

# Ensure HTTPS dev certs are trusted (safe to no-op if already trusted)
dotnet dev-certs https >/dev/null 2>&1 || true

echo "Starting EpicPetEMR backend, shared library, and Blazor UI..."
echo "Press Ctrl+C to stop all."

# Array to hold process IDs
pids=()

# Shared library doesn’t have a runnable target but we include watch for rebuilds
dotnet watch --project "$SHARED" build &
pids+=($!)

dotnet watch --project "$BACKEND" run &
pids+=($!)

dotnet watch --project "$UI" run &
pids+=($!)

# Graceful shutdown on Ctrl+C
cleanup() {
  echo -e "\nStopping all processes..."
  for pid in "${pids[@]}"; do
    kill "$pid" 2>/dev/null || true
  done
  wait
}
trap cleanup INT TERM

wait
