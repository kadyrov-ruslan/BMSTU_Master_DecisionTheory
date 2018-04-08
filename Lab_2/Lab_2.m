 M = dlmread('hungarian.data.txt');
[rowsNum, colsNum]= size(M);

paramsCount = 14;
inputMatrix = zeros(rowsNum/10, paramsCount);

tempArr = zeros;
tempArrIdx = 1;
for j=1:rowsNum
    for i=1:colsNum
        tempArr(tempArrIdx) = M(j,i);
        tempArrIdx = tempArrIdx + 1;
    end
end

%Get only `paramsCount` important params for clustering
for j=1:(rowsNum/10)
    tempElements = tempArr((1 + (j - 1)*80):80*j);
    inputMatrix(j,1) = tempElements(1,3);
    inputMatrix(j,2) = tempElements(1,4);
    inputMatrix(j,3) = tempElements(1,9);
    inputMatrix(j,4) = tempElements(1,10);
    inputMatrix(j,5) = tempElements(1,12);
    inputMatrix(j,6) = tempElements(1,16);
    inputMatrix(j,7) = tempElements(1,19);
    inputMatrix(j,8) = tempElements(1,32);
    inputMatrix(j,9) = tempElements(1,38);
    inputMatrix(j,10) = tempElements(1,40);
    inputMatrix(j,11) = tempElements(1,41);
    inputMatrix(j,12) = tempElements(1,44);
    inputMatrix(j,13) = tempElements(1,51);
    inputMatrix(j,14) = tempElements(1,58);
end

[IDX,C] = kmeans(inputMatrix,5);

figure
scatter3(inputMatrix(:,1),inputMatrix(:,4),inputMatrix(:,12), 40, IDX, 'filled')

% X = [IDX(1,:),IDX(2,:),IDX(3,:),IDX(4,:),IDX(5,:)];
% varNames = {'1Param'; '2Param'; '3Param'; '4Param'; '5Param'};
% figure
% plotmatrix(inputMatrix);
% text([.08 .24 .43 .66 .83], repmat(-.1,1,5), varNames, 'FontSize',8);
% text(repmat(-.12,1,5), [.86 .62 .41 .25 .02], varNames, 'FontSize',8, 'Rotation',90);