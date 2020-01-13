from pyscipopt import Model

model = Model("migros-cross-math")
x = []
for i in range(0, 9):
    x.append(model.addVar(f'x{i}', vtype="INTEGER"))
constraints = []

M = 1_000_000
epsilon = 0.0001
ycnt = 0

# general cross-math rules

for x_i in x:
    constraints.append(x_i >= 1)
    constraints.append(x_i <= 9)
    for x_j in x:
        y = model.addVar(f'helper_{ycnt}', vtype="BINARY")
        ycnt += 1
        constraints.append(x_i - x_j <= -epsilon + y * M)
        constraints.append(x_i - x_j >= epsilon - (1-y) * M)

# actual cross-math
constraints.append((x[0] - x[1]) * x[2] == 3)
constraints.append((x[3] + x[4]) * x[5] == 20)
constraints.append((x[6] + x[7]) + x[8] == 20)

constraints.append(x[0] - x[3] + x[6] == 13)
constraints.append((x[1] + x[4]) / x[7] == 1)
constraints.append((x[2] * x[5]) * x[8] == 20)

model.setObjective(0)
for cons in constraints:
    model.addCons(cons)

model.optimize()
sol = model.getBestSol()
for xi in x:
    print(f'i: {sol[xi]}')