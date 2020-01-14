from pyscipopt import Model

model = Model("migros-cross-math")
x = []
for i in range(0, 9):
    x.append([])
    for j in range(0, 9):
        x[i].append(model.addVar(f'x{i}_{j}', vtype="BINARY"))

constraints = []

# general cross-math rules

for x_i in x:
    constr = 0
    for x_i_j in x_i:
        constr += x_i_j
    constraints.append(constr == 1)

for j in range(0, len(x_i)):
    constr = 0
    for i in range(0, len(x)):
        constr += x[i][j]
    constraints.append(constr == 1)

vals = []
for x_i in x:
    val = 0
    for j in range(0, len(x_i)):
        val += (j + 1) * x_i[j]
    vals.append(val)
# actual cross-math
constraints.append((vals[0] - vals[1]) * vals[2] == 3)
constraints.append((vals[3] + vals[4]) * vals[5] == 20)
constraints.append((vals[6] + vals[7]) + vals[8] == 20)

constraints.append(vals[0] - vals[3] + vals[6] == 13)
constraints.append((vals[1] + vals[4]) / vals[7] == 1)
constraints.append((vals[2] * vals[5]) * vals[8] == 20)

model.setObjective(0)
for cons in constraints:
    model.addCons(cons)

model.optimize()
sol = model.getBestSol()
for i in range(0, len(x)):

    for j in range(0, len(x[i])):
        if sol[x[i][j]] == 1:
            print(j + 1, end='')
    if (i + 1) % 3 == 0:
        print()
